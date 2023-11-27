using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IPDFService
    {
        byte[] HtmlToPDF(string html, BalanceDtoPresentacion balance);
    }
}
