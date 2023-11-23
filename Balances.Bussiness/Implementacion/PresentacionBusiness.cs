using Balances.Bussiness.Contrato;
using Balances.DTO;
using EmailSender;
using Microsoft.AspNetCore.Hosting;

namespace Balances.Bussiness.Implementacion
{
    public class PresentacionBusiness : IPresentacionBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;


        private readonly IEmailSenderService _emailSenderService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public PresentacionBusiness(IBalanceBusiness balanceBusiness,
            IEmailSenderService emailSenderService,
            IWebHostEnvironment webHostEnvironment)
        {
            _balanceBusiness = balanceBusiness;


            _emailSenderService = emailSenderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public ResponseDTO<BalanceDto> PresentarTramite()
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            //busco balance
            var bal = _balanceBusiness.BalanceActual;
            var html = PlantillaPresentacionHtml(bal);

            var EmailRequest = _emailSenderService.EmailPresentacion(bal, html);

            try
            {
                _emailSenderService.SendEmailAsync(EmailRequest);

                respuesta.Message = "Presentacion generada y enviada correctamente";
                respuesta.Result = bal;
                respuesta.IsSuccess = true;

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
            }


            return respuesta;
        }

        public string PlantillaPresentacionHtml(BalanceDto balance)
        {
            var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
            var Plantilla = path + "/PlantillaPresentacionBalance.html";

            ////var Plantilla = path + "/PlantillaPresentacion.html";
            //var Plantilla = path + ROUTEHTML;

            var PlantillaHTML = File.ReadAllText(Plantilla);


            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);


            return PlantillaHTML;
        }
    }
}
