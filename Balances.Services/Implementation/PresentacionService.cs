using Balances.Services.Contract;
using Microsoft.AspNetCore.Hosting;

namespace Balances.Services.Implementation
{
    public class PresentacionService : IPresentacionService
    {
        private readonly IBalanceService _balanceService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PresentacionService(IBalanceService balanceService, IWebHostEnvironment webHostEnvironment)
        {
            _balanceService = balanceService;
            _webHostEnvironment = webHostEnvironment;
        }

        public string PresentacionBalance(string balanceId)
        {
            var balance = _balanceService.GetById(balanceId);
            var path = _webHostEnvironment.WebRootPath + "/../Plantillas";

            var Plantilla = path + "/PlantillaPresentacion.html";

            var PlantillaHTML = File.ReadAllText(Plantilla);

            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Entidad.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Entidad.Domicilio);




            return PlantillaHTML;
        }
    }
}
