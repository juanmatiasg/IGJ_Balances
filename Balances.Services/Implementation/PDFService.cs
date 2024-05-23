using Balances.DTO;
using Balances.Services.Contract;
using iText.Html2pdf;
using Microsoft.Extensions.Logging;

namespace Balances.Services.Implementation
{
    public class PDFService : IPDFService
    {
        private readonly ILogger<QRService> _logger;


        public PDFService(ILogger<QRService> logger)
        {
            _logger = logger;
        }

        public byte[] HtmlToPDF(string html, BalanceDto balance)
        {

            try
            {
                byte[] bytes;

                using (var memoryStream = new MemoryStream())
                {


                    HtmlConverter.ConvertToPdf(html.ToString(), memoryStream);


                    bytes = memoryStream.ToArray();
                }


                return bytes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PDFService.HtmlToPDF : \n {ex}");
                throw ex;
            }


        }
    }
}


