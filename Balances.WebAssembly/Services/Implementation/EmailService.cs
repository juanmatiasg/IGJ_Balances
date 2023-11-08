using Balances.DTO;
using Balances.WebAssembly.Services.Contract;
using Dominio.Helpers;
using EmailSender;
using System.Net.Http.Json;

namespace Balances.WebAssembly.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<string>> SendEmail(string email)
        {
         
          return await _httpClient.GetFromJsonAsync<ResponseDTO<String>>($"mail?email={email}");
            
        }
    }
}
