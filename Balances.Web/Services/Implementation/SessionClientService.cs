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
        public async Task<ResponseDTO<bool>> setBalanceId(string sessionId, string balanceId)

        {
            var responseDto = new ResponseDTO<bool>();
            try
            {

                var rsp = await _httpClient.GetFromJsonAsync<ResponseDTO<bool>>($"Session/SetBalanceId?sesionId={sessionId}&balanceId={balanceId}");


                responseDto.Result = rsp.Result;
                responseDto.IsSuccess = rsp.IsSuccess;
                responseDto.Message = rsp.Message;



            }
            catch (Exception ex)
            {

                responseDto.Result = false;
                responseDto.IsSuccess = false;
                responseDto.Message = $"{"Error:SetBalanceId" + ex.Message}";
            }

            return responseDto;
        }
        //public async Task<string> getBalanceId(string sessionId)
        //{

        //    string rsp;

        //    try
        //    {

        //        rsp = await _httpClient.GetFromJsonAsync<string>($"Session/getBalanceId?sesionId={sessionId}");


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //    return rsp;
        //}

        public async Task<string> getBalanceId(string sessionId)
        {
            string rsp;
            //var responseDto = new ResponseDTO<string>();

            try
            {
                //var rsp = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getBalanceId?sesionId={sessionId}");
                rsp = await _httpClient.GetStringAsync($"Session/getBalanceId?sesionId={sessionId}");

                //// Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                //var result = await rsp.Content.ReadFromJsonAsync<ResponseDTO<string>>();


                //responseDto.Result = rsp.Result;
                //responseDto.IsSuccess = rsp.IsSuccess;
                //responseDto.Message = rsp.Message;

            }
            catch (Exception ex)
            {

                throw ex;
                //responseDto.Result = null;
                //responseDto.Message = $"{"Error:getBalanceId" + ex.Message}";
                //responseDto.IsSuccess = false;
            }
            return rsp;

            //return responseDto;
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




