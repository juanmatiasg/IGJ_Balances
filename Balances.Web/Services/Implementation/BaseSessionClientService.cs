using Balances.DTO;
using Balances.Utilities;
using Balances.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class BaseSessionClientService : IBaseSessionClientService
    {
        private readonly HttpClient _httpClient;

        public BaseSessionClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<string>> getSession()
        {

            try
            {
                var rst = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getSession");
                var balanceId = SessionStorageHelper.GetBalanceId(rst.Result);
                return new ResponseDTO<string>
                {
                    Result = balanceId,
                    IsSuccess = rst.IsSuccess,
                    Message = rst.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"{"Error:GetSession" + ex.Message}"
                };
            }
        }

        public async Task<ResponseDTO<string>> setSession(string balanceid)
        {
            var responseDto = new ResponseDTO<string>();
            try
            {

                var response = await _httpClient.PostAsJsonAsync($"Session/{balanceid}", balanceid);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<string>>();

                if (result.IsSuccess)
                {


                    responseDto.Result = result.Result;
                    responseDto.Message = result.Message;
                    responseDto.IsSuccess = result.IsSuccess;

                }

            }
            catch (Exception ex)
            {


                responseDto.Result = null;
                responseDto.Message = $"{"Error:SetSession" + ex.Message}";
                responseDto.IsSuccess = false;

            }
            return responseDto;
        }
    }
}

