using Balances.DTO;
using Balances.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class SessionClientService : ISessionClientService
    {
        private readonly HttpClient _httpClient;
        public string SessionId { get; }


        public SessionClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<string>> getNewSession()
        {
            var responseDto = new ResponseDTO<string>();

            try
            {

                var rst = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getNewSession");



                responseDto.Result = rst.Result;
                responseDto.IsSuccess = rst.IsSuccess;
                responseDto.Message = rst.Message;


            }
            catch (Exception ex)
            {

                responseDto.Result = null;
                responseDto.IsSuccess = false;
                responseDto.Message = $"{"Error:GetNewSession" + ex.Message}";
            }

            return responseDto;
        }
        public async Task<bool> setBalanceId(string sessionId, string balanceId)

        {
            bool rsp = false;
            //var responseDto = new ResponseDTO<bool>();
            try
            {

                //var rsp = await _httpClient.GetFromJsonAsync<ResponseDTO<bool>>($"Session/SetBalanceId?sesionId={sessionId}&balanceId={balanceId}");

                var rst = await _httpClient.GetAsync($"Session/SetBalanceId?sesionId={sessionId}&balanceId={balanceId}");
                if (rst.StatusCode == System.Net.HttpStatusCode.OK) rsp = true;

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return rsp;
        }


        public async Task<string> getBalanceId(string sessionId)
        {
            string rsp;


            try
            {

                rsp = await _httpClient.GetStringAsync($"Session/getBalanceId?sesionId={sessionId}");


            }
            catch (Exception ex)
            {

                throw ex;

            }
            return rsp;


        }



        public async Task<ResponseDTO<string>> setSession()
        {
            var responseDto = new ResponseDTO<string>();
            try
            {

                var rsp = await _httpClient.GetAsync($"Session/getNewSession");

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await rsp.Content.ReadFromJsonAsync<ResponseDTO<string>>();

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


        public async Task<string> GetSessionBalanceId()
        {

            var balanceIdSesion = await _httpClient.GetStringAsync($"Session/getBalanceIdSession");

            return balanceIdSesion;
        }



    }
}

//public async Task<string> GetSessionToken()
//{

//    var tokenSession = await _httpClient.GetStringAsync($"Session/getTokenSession");


//    return tokenSession;
//}




