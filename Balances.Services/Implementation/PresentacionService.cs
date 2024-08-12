using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Hosting;

namespace Balances.Services.Implementation
{
    public class PresentacionService : IPresentacionService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string PlantillaHTML { get; private set; }

        public PresentacionService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public string CrearPlantillaPresentacionEmail(BalanceDto balance, string qr)
        {
            AutoridadDto autoridadfirmante = balance.Autoridades.Where(_ => _.EsFirmante == true).First();

            //CAMBIA LA PLANTILLA POR LA VISUALIZACION DE LAS IMAGENES EN EL EMAIL
            this.PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalanceEMAIL.html");
            SetTag("{{Id}}", balance.Id);
            SetTag("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            SetTag("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            SetTag("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            SetTag("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            SetTag("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            SetTag("{{Rectificatorio}}", balance.Caratula.Rectificatorio ? "Rectificación" : "Original");
            SetTag("{{RepresentanteLegal.RepresentanteApellido}}", autoridadfirmante.Apellido);
            SetTag("{{RepresentanteLegal.RepresentanteNombre}}", autoridadfirmante.Nombre);
            SetTag("{{RepresentanteLegal.RepresentanteNroFiscal}}", autoridadfirmante.NroFiscal);
            SetTag("{{Contador.ContadorApellido}}", balance.Contador.Apellido);
            SetTag("{{Contador.ContadorNombre}}", balance.Contador.Nombre);
            SetTag("{{Contador.ContadorNroFiscal}}", balance.Contador.NroFiscal);
            SetTag("{{Contador.Folio}}", balance.Contador.Folio);
            SetTag("{{Contador.Tomo}}", balance.Contador.Tomo);
            SetTag("{{Contador.FechaInformeAuditorExt}}", balance.Contador.FechaInformeAuditorExt.ToShortDateString());
            SetTag("{{Contador.NroLegalInfoAudExt}}", balance.Contador.NroLegalInfoAudExt);
            SetTag("{{Contador.Opinion}}", balance.Contador.Opinion);

            if (balance.Contador.Observaciones != null) { SetTag("{{Contador.Observaciones}}", balance.Contador.Observaciones); }
            else { SetTag("{{Contador.Observaciones}}", "----"); }

            if (balance.Contador.EsSocioEstudio) { SetTag("{{Contador.EsSocioEstudio}} ", "En carácter de socio estudio  "); }
            else { SetTag("{{Contador.EsSocioEstudio}}", " "); }

            if (balance.Contador.TomoEstudio != null) { SetTag("{{Contador.TomoEstudio}} ", $"T°{balance.Contador.TomoEstudio}"); }
            else { SetTag("{{Contador.TomoEstudio}}", " "); }

            if (balance.Contador.FolioEstudio != null) { SetTag("{{Contador.FolioEstudio}} ", $" F° {balance.Contador.FolioEstudio}"); }
            else { SetTag("{{Contador.FolioEstudio }}", " "); }


            SetTag("Memoria", balance.Libros.Memoria);
            SetTag("PatrimonioNeto", balance.Libros.PatrimonioNeto);
            SetTag("Asamblea", balance.Libros.Asamblea);
            SetTag("SituacionPatrimonial", balance.Libros.SituacionPatrimonial);
            SetTag("Auditor", balance.Libros.Auditor);
            SetTag("Administracion", balance.Libros.Administracion);
            SetTag("Resultados", balance.Libros.Resultados);
            SetTag("Efectivo", balance.Libros.Efectivo);
            SetTag("Informacion", balance.Libros.Informacion);
            SetTag("EstadosContablesConsolidados", balance.Libros.EstadosContablesConsolidados);
            SetTag("Fiscalizacion", balance.Libros.Fiscalizacion);
            SetTag("AsistenciaAsamblea", balance.Libros.AsistenciaAsamblea);
            //SetTag("IVA", balance.Libros.IVA);
            SetTag("IVACompras", balance.Libros.IVACompras);
            SetTag("IVAVentas", balance.Libros.IVAVentas);


            return PlantillaHTML;

        }

        private void SetTag(string libroNombre, LibroDto libro)
        {
            // Esto lo agrego Juan Fecha 18/07/2024
            SetTag("{{" + libroNombre + ".Nombre}}", libro.Nombre);
            SetTag("{{" + libroNombre + ".NumeroRubrica}}", libro.NumeroRubrica);
            SetTag("{{" + libroNombre + ".FolioObraTranscripcion}}", libro.FolioObraTranscripcion);
            SetTag("{{" + libroNombre + ".Denominacion}}", libro.Nombre);
            SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", libro.FechaUltimaRegistracion?.ToString("dd/MM/yyyy"));
            SetTag("{{" + libroNombre + ".FolioUltimaRegistracion}}", libro.FolioUltimaRegistracion);

            if (libro.Nombre != null)
            {
                SetTag("{{" + libroNombre + ".Nombre}}", libro.Nombre);
                SetTag("{{" + libroNombre + ".NumeroRubrica}}", libro.NumeroRubrica);
                SetTag("{{" + libroNombre + ".FolioObraTranscripcionn}}", libro.FolioObraTranscripcion);
                SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", libro.FechaUltimaRegistracion?.ToString("dd/MM/yyyy"));
                SetTag("{{" + libroNombre + ".FolioUltimaRegistracion}}", libro.FolioUltimaRegistracion);
            }
            else
            {
                SetTag("{{" + libroNombre + ".Nombre}}", "N/C");
                SetTag("{{" + libroNombre + ".NumeroRubrica}}", "N/C");
                SetTag("{{" + libroNombre + ".FolioObraTranscripcion}}", "N/C");
                SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", "N/C");
                SetTag("{{" + libroNombre + ".FolioUltimaRegistracion}}", "N/C");
            }



        }

        private void SetTag(string tag, string valor)
        {
            this.PlantillaHTML = PlantillaHTML.Replace(tag, valor);

        }

        public string CrearPlantillaPresentacionPdf(BalanceDto balance, string qr)
        {
            AutoridadDto autoridadfirmante = balance.Autoridades.Where(_ => _.EsFirmante == true).First();
            //CAMBIA LA PLANTILLA POR LA VISUALIZACION DE LAS IMAGENES EN EL PDF
            this.PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalancePDF.html");

            PlantillaHTML = PlantillaHTML.Replace("{{Id}}", balance.Id);
            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            PlantillaHTML = PlantillaHTML.Replace("{{Rectificatorio}}", balance.Caratula.Rectificatorio ? "Rectificación" : "original");
            PlantillaHTML = PlantillaHTML.Replace("{{QR}}", qr);
            PlantillaHTML = PlantillaHTML.Replace("{{RepresentanteLegal.RepresentanteApellido}}", autoridadfirmante.Apellido);
            PlantillaHTML = PlantillaHTML.Replace("{{RepresentanteLegal.RepresentanteNombre}}", autoridadfirmante.Nombre);
            PlantillaHTML = PlantillaHTML.Replace("{{RepresentanteLegal.RepresentanteNroFiscal}}", autoridadfirmante.NroFiscal);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorApellido}}", balance.Contador.Apellido);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorNombre}}", balance.Contador.Nombre);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorNroFiscal}}", balance.Contador.NroFiscal);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.Folio}}", balance.Contador.Folio);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.Tomo}}", balance.Contador.Tomo);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.FechaInformeAuditorExt}}", balance.Contador.FechaInformeAuditorExt.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.NroLegalInfoAudExt}}", balance.Contador.NroLegalInfoAudExt);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.Opinion}}", balance.Contador.Opinion);


            if (balance.Contador.Observaciones != null) { SetTag("{{Contador.Observaciones}}", balance.Contador.Observaciones); }
            else { SetTag("{{Contador.Observaciones}}", "----"); }

            if (balance.Contador.EsSocioEstudio) { SetTag("{{Contador.EsSocioEstudio}} ", "En carácter de socio estudio  "); }
            else { SetTag("{{Contador.EsSocioEstudio}}", " "); }

            if (balance.Contador.TomoEstudio != null) { SetTag("{{Contador.TomoEstudio}} ", $"T°{balance.Contador.TomoEstudio}"); }
            else { SetTag("{{Contador.TomoEstudio}}", " "); }

            if (balance.Contador.FolioEstudio != null) { SetTag("{{Contador.FolioEstudio}} ", $" F° {balance.Contador.FolioEstudio}"); }
            else { SetTag("{{Contador.FolioEstudio }}", " "); }


            SetTag("Memoria", balance.Libros.Memoria);
            SetTag("PatrimonioNeto", balance.Libros.PatrimonioNeto);
            SetTag("Asamblea", balance.Libros.Asamblea);
            SetTag("SituacionPatrimonial", balance.Libros.SituacionPatrimonial);
            SetTag("Auditor", balance.Libros.Auditor);
            SetTag("Administracion", balance.Libros.Administracion);
            SetTag("Resultados", balance.Libros.Resultados);
            SetTag("Efectivo", balance.Libros.Efectivo);
            SetTag("Informacion", balance.Libros.Informacion);
            SetTag("EstadosContablesConsolidados", balance.Libros.EstadosContablesConsolidados);
            SetTag("Fiscalizacion", balance.Libros.Fiscalizacion);
            SetTag("AsistenciaAsamblea", balance.Libros.AsistenciaAsamblea);
            //SetTag("IVA", balance.Libros.IVA);
            SetTag("IVACompras", balance.Libros.IVACompras);
            SetTag("IVAVentas", balance.Libros.IVAVentas);






            return PlantillaHTML;


        }
        private string GetPlantillaHtml(string plantilla)
        {
            var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
            var Plantilla = path + "/" + plantilla;

            var PlantillaHTML = System.IO.File.ReadAllText(Plantilla);
            return PlantillaHTML;
        }
    }
}
