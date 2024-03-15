using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class PresentacionService:IPresentacionService
    {
        private readonly HttpClient _httpClient;
        public PresentacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<BalanceDto>> generarPresentacion()
        {

            try
            {
                var result = await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Presentacion/GenerarPresentacion");

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
    }






}
