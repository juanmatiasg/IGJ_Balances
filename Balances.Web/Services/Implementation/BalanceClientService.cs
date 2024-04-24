using Balances.DTO;
using Balances.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class BalanceClientService : IBalanceClientService
    {
        private readonly HttpClient _httpClient;

        public BalanceClientService(HttpClient httpClient/*, SessionClientService session*/)
        {
            _httpClient = httpClient;
            //session.SessionId = session.SessionId;
        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            var balance = await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

            return balance;
        }
    }
}
