using Balances.DTO;


namespace Balances.WebAssembly.Services.Contract
{
    public interface  IEmailService
    {
        Task <ResponseDTO<String>> SendEmail(String email);
    }
}
