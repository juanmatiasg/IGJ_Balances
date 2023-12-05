using Balances.DTO;
using Balances.Services.Contract;
using iText.Html2pdf;

namespace Balances.Services.Implementation
{
    public class PDFService : IPDFService
    {
        public byte[] HtmlToPDF(string html, BalanceDtoPresentacion balance)
        {


            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {


                HtmlConverter.ConvertToPdf(html.ToString(), memoryStream);
                //iText.Html2pdf.HtmlConverter.ConvertToPdf(html, memoryStream);

                bytes = memoryStream.ToArray();
            }
            // writing PDF output to file for testing

            //string base64PDF = System.Convert.ToBase64String(bytes, 0, bytes.Length);
            //string str = "<embed src='data:application/pdf;base64, " + base64PDF + "' type='application/pdf' width='500px' height='800px' />";
            //pdfdiv.InnerHtml = str;

            return bytes;
        }
    }
}


