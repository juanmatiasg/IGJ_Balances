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

        public BalanceDtoPresentacion GetBalanceAutoridadySocioFirmante(BalanceDto balance)
        {
            var balFilter = new BalanceDtoPresentacion();



            balFilter.Autoridad = balance.Autoridades.FirstOrDefault(x => x.EsFirmante == true);
            balFilter.Socios = new SociosDto();

            balFilter.Socios.PersonasHumanas = new List<PersonaHumanaDto>();

            balFilter.Socios.PersonasHumanas.Add(balance.Socios.PersonasHumanas.FirstOrDefault(x => x.EsFirmante == true));
            balFilter.Socio = balance.Socios.PersonasHumanas.FirstOrDefault(x => x.EsFirmante == true);

            balFilter.Socios.PersonasJuridicas = new List<PersonaJuridicaDto>();
            balFilter.Socios.PersonasJuridicas = balance.Socios.PersonasJuridicas;
            balFilter.Caratula = balance.Caratula;
            balFilter.Contador = balance.Contador;
            balFilter.Id = balance.Id;
            balFilter.Archivos = balance.Archivos;
            balFilter.EstadoContable = balance.EstadoContable;
            balFilter.Libros = balance.Libros;
            balance.Socios.PersonasJuridicas = balance.Socios.PersonasJuridicas;


            return balFilter;
        }

        public string CrearPlantillaPresentacionEmail(BalanceDtoPresentacion balance, string qr)
        {
            this.PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalance2.html");

            SetTag("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            SetTag("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            SetTag("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            SetTag("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            SetTag("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            // SetTagPlantilla("{{QR}}", qr);
            SetTag("{{RepresentanteLegal.RepresentanteApellido}}", balance.Autoridad.Apellido);
            SetTag("{{RepresentanteLegal.RepresentanteNombre}}", balance.Autoridad.Nombre);
            SetTag("{{representanteLegal.RepresentanteNroFiscal}}", balance.Autoridad.NroFiscal);
            SetTag("{{Contador.ContadorApellido}}", balance.Contador.Apellido);
            SetTag("{{Contador.ContadorNombre}}", balance.Contador.Nombre);
            SetTag("{{Contador.ContadorNroFiscal}}", balance.Contador.NroFiscal);
            SetTag("{{Contador.Folio}}", balance.Contador.Folio);
            SetTag("{{Contador.Tomo}}", balance.Contador.Tomo);
            SetTag("{{Contador.FechaInformeAuditorExt}}", balance.Contador.FechaInformeAuditorExt.ToShortDateString());
            SetTag("{{Contador.NroLegalInfoAudExt}}", balance.Contador.NroLegalInfoAudExt);

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
            SetTag("IVA", balance.Libros.IVA);
            SetTag("IVACompras", balance.Libros.IVACompras);
            SetTag("IVAVentas", balance.Libros.IVAVentas);


            return PlantillaHTML;

        }

        private void SetTag(string libroNombre, LibroDto libro)
        {
            // Esto lo agrego Juan Fecha 18/07/2024
            SetTag("{{" + libroNombre + ".Nombre}}", libro.Nombre);
            SetTag("{{" + libroNombre + ".NumeroRubrica}}", libro.NumeroRubrica);
            SetTag("{{" + libroNombre + ".Folio}}", libro.Folio);
            SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", libro.FechaUltimaRegistracion.ToString("dd/MM/yyyy"));

            if (libro.Nombre != null)
            {
                SetTag("{{" + libroNombre + ".Nombre}}", libro.Nombre);
                SetTag("{{" + libroNombre + ".NumeroRubrica}}", libro.NumeroRubrica);
                SetTag("{{" + libroNombre + ".Folio}}", libro.Folio);
                SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", libro.FechaUltimaRegistracion.ToString("dd/MM/yyyy"));
            }
            else
            {
                SetTag("{{" + libroNombre + ".Nombre}}", "N/C");
                SetTag("{{" + libroNombre + ".NumeroRubrica}}", "N/C");
                SetTag("{{" + libroNombre + ".Folio}}", "N/C");
                SetTag("{{" + libroNombre + ".FechaUltimaRegistracion}}", "N/C");
            }

      

        }

        private void SetTag(string tag, string valor)
        {
            this.PlantillaHTML = PlantillaHTML.Replace(tag, valor);

        }

        public string CrearPlantillaPresentacionPdf(BalanceDtoPresentacion balance, string qr)
        {
            this.PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalance2.html");

            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            PlantillaHTML = PlantillaHTML.Replace("{{QR}}", qr);
            PlantillaHTML = PlantillaHTML.Replace("{{RepresentanteLegal.RepresentanteApellido}}", balance.Autoridad.Apellido);
            PlantillaHTML = PlantillaHTML.Replace("{{RepresentanteLegal.RepresentanteNombre}}", balance.Autoridad.Nombre);
            PlantillaHTML = PlantillaHTML.Replace("{{representanteLegal.RepresentanteNroFiscal}}", balance.Autoridad.NroFiscal);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorApellido}}", balance.Contador.Apellido);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorNombre}}", balance.Contador.Nombre);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.ContadorNroFiscal}}", balance.Contador.NroFiscal);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.Folio}}", balance.Contador.Folio);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.Tomo}}", balance.Contador.Tomo);
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.FechaInformeAuditorExt}}", balance.Contador.FechaInformeAuditorExt.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Contador.NroLegalInfoAudExt}}", balance.Contador.NroLegalInfoAudExt);


            SetTag("Memoria", balance.Libros.Memoria);
            SetTag("PatrimonioNeto", balance.Libros.PatrimonioNeto);
            SetTag("SituacionPatrimonial", balance.Libros.SituacionPatrimonial);
            SetTag("Auditor", balance.Libros.Auditor);
            SetTag("Administracion", balance.Libros.Administracion);
            SetTag("Resultados", balance.Libros.Resultados);
            SetTag("Efectivo", balance.Libros.Efectivo);
            SetTag("Informacion", balance.Libros.Informacion);
            SetTag("EstadosContablesConsolidados", balance.Libros.EstadosContablesConsolidados);
            SetTag("Fiscalizacion", balance.Libros.Fiscalizacion);
            SetTag("AsistenciaAsamblea", balance.Libros.AsistenciaAsamblea);
            SetTag("IVA", balance.Libros.IVA);
            SetTag("IVACompras", balance.Libros.IVACompras);
            SetTag("IVAVentas", balance.Libros.IVAVentas);






            return PlantillaHTML;


        }
        private string GetPlantillaHtml(string plantilla)
        {
            var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
            var Plantilla = path + "/" + plantilla;

            var PlantillaHTML = File.ReadAllText(Plantilla);
            return PlantillaHTML;
        }
    }
}
