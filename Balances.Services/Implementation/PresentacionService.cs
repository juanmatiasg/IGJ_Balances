using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Hosting;

namespace Balances.Services.Implementation
{
    public class PresentacionService : IPresentacionService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

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

        public string CrearPlantillaPresentacion(BalanceDtoPresentacion balance, string qr)
        {
            string PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalance2.html");

            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            // PlantillaHTML = PlantillaHTML.Replace("{{QR}}", qr);
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






            return PlantillaHTML;

        }

        public string PlantillaPresentacionHtml(BalanceDtoPresentacion balance, string qr)
        {
            string PlantillaHTML = GetPlantillaHtml("PlantillaPresentacionBalance.html");

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
