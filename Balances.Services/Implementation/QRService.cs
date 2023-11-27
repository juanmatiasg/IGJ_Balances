using Balances.Services.Contract;
using QRCoder;

namespace Balances.Services.Implementation
{
    public class QRService : IQRService
    {

        public string QRGenerator(string cadena)
        {
            //CREAR QR CON DATA
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrData = QRCodeGenerator.GenerateQrCode(cadena, QRCodeGenerator.ECCLevel.Q);

            //VISUALIZACION DEL QR
            PngByteQRCode qrCode = new PngByteQRCode(qrData);
            byte[] qrCodeImage = qrCode.GetGraphic(5);

            //VISUALIZAR EN BASE  64
            string model = Convert.ToBase64String(qrCodeImage);
            return model;
        }
    }
}
