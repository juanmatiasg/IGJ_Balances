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
            var responseDto = new ResponseDTO<string>();

            try
            {

                var rst = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getSession");
                var balanceId = SessionStorageHelper.GetBalanceId(rst.Result);


                responseDto.Result = balanceId;
                responseDto.IsSuccess = rst.IsSuccess;
                responseDto.Message = rst.Message;


            }
            catch (Exception ex)
            {

                responseDto.Result = null;
                responseDto.IsSuccess = false;
                responseDto.Message = $"{"Error:GetSession" + ex.Message}";
            }

            return responseDto;
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

        public async Task<string> GetSessionToken()
        {

            var tokenSession = await _httpClient.GetStringAsync($"Session/getTokenSession");


            return tokenSession;
        }

        public async Task<string> GetSessionBalanceId()
        {

            var balanceIdSesion = await _httpClient.GetStringAsync($"Session/getBalanceIdSession");

            return balanceIdSesion;
        }
    }
}

