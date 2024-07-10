using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using QRCoder;

namespace Balances.Services.Implementation
{
    public class QRService : IQRService
    {
        private readonly ILogger<QRService> _logger;

        public QRService(ILogger<QRService> logger)
        {
            _logger = logger;
        }

        public string QRGenerator(string hash)
        {

            try
            {
                //CREAR QR CON DATA
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrData = QRCodeGenerator.GenerateQrCode(hash, QRCodeGenerator.ECCLevel.Q);

                //VISUALIZACION DEL QR
                PngByteQRCode qrCode = new PngByteQRCode(qrData);
                byte[] qrCodeImage = qrCode.GetGraphic(5);

                //VISUALIZAR EN BASE  64
                string model = Convert.ToBase64String(qrCodeImage);
                return model;


            }
            catch (Exception ex)
            {
                _logger.LogError($"QRService.QRGenerator : \n {ex}");
                throw ex;
            }

        }
    }
}
