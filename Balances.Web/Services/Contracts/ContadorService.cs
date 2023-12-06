using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class ContadorService : IContadorService
    {
        private readonly HttpClient _httpClient;

        public ContadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");
        }

        public async Task<ResponseDTO<BalanceDto>> getContador(string idBalance)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Contador/{idBalance}");

        }

        public async Task<ResponseDTO<string>> getSession()
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<string>>("Session/getSession");
        }

        public async Task<ResponseDTO<BalanceDto>> postContador(ContadorDto contador)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {


                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Contador/Insert", contador);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();


                rsp = result;
                rsp.IsSuccess = true;


            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                rsp.Message = ex.Message;

            }

            return rsp;
        }

        public async Task<ResponseDTO<string>> setSession(string idBalance)
        {
            try
            {
                var session = new ResponseDTO<string>
                {
                    Result = idBalance,
                    Message = "Session Created",
                    IsSuccess = true,
                };



                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync($"Session/{idBalance}", session);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<string>>();

                return new ResponseDTO<string>
                {
                    Result = result.Result,
                    IsSuccess = true,
                    Message = result.Message
                };


            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Error in the request"
                };
            }
        }
    }
}
