using Balances.Bussiness.Contrato;
using Balances.DTO;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using QRCoder;

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

            var qr = QRGenerator(bal.Id);
            // lleno la plantilla con los datos del balance

            var Plantillahtml = PlantillaPresentacionHtml(bal, qr);

            // paso como parametro el balance y la plantilla para armar el emailRequest 
            var EmailRequest = _emailSenderService.EmailPresentacion(bal, Plantillahtml);

            try
            {
                //Envio el email con los datos del EmailRequest
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

        public string FormatPresentacionHTML()
        {

            //busco balance
            var bal = _balanceBusiness.BalanceActual;
            // lleno la plantilla con los datos del balance
            var qr = QRGenerator(bal.Id);
            var Plantillahtml = PlantillaPresentacionHtml(bal, qr);


            // paso como parametro el balance y la plantilla para armar el emailRequest 
            //var EmailRequest = _emailSenderService.EmailPresentacion(bal, Plantillahtml);

            try
            {
                //Envio el email con los datos del EmailRequest
                //_emailSenderService.SendEmailAsync(EmailRequest);

                //respuesta.Message = "Presentacion generada y enviada correctamente";
                //respuesta.Result = Plantillahtml;
                //respuesta.IsSuccess = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return Plantillahtml;
        }

        public string PlantillaPresentacionHtml(BalanceDto balance, string qr)
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
            PlantillaHTML = PlantillaHTML.Replace("{{QR}}", qr);


            return PlantillaHTML;
        }

        public string QRGenerator(string id)
        {
            ;
            //CREAR QR CON DATA
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrData = QRCodeGenerator.GenerateQrCode(id, QRCodeGenerator.ECCLevel.Q);

            //VISUALIZACION DEL QR
            PngByteQRCode qrCode = new PngByteQRCode(qrData);
            byte[] qrCodeImage = qrCode.GetGraphic(5);

            //VISUALIZAR EN BASE  64
            string model = Convert.ToBase64String(qrCodeImage);
            return model;

        }
    }
}
