using Balances.DTO;
using Balances.Model;
using Balances.Web.Services.Implementation;
using MongoDB.Bson;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            try
            {
                

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Contador/Insert", contador);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                
                return new ResponseDTO<BalanceDto>
                {
                    Result = result.Result,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };

              
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Error in the request"
                };
            }
        }

        public async Task<ResponseDTO<string>> setSession(string idBalance)
        {
            try
            {
                var session = new ResponseDTO<string>
                {
                  Result = idBalance,
                  Message ="Session Created",
                  IsSuccess=true,
                };



                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync($"Session/{idBalance}",session);

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
