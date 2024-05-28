using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class PresentacionClientService : IPresentacionClientService
    {
        private readonly HttpClient _httpClient;
        public PresentacionClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<BalanceDto>> generarPresentacion(string sesionId)
        {

            try
            {
                var result = await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Presentacion/GenerarPresentacion/{sesionId}");

                return new ResponseDTO<BalanceDto>
                {
                    Result = result.Result,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<string> GenerarPresentacionEnHtml(string sesionId)
        {
            string html = await _httpClient.GetStringAsync($"Presentacion/GenerarPresentacionEnHtml/{sesionId}");
            return html;

        }
    }
}
