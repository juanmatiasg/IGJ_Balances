using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class LibrosService : ILibrosService
    {
        private readonly HttpClient _httpClient;
        public LibrosService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

        }

        public async Task<ResponseDTO<string>> getSession()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getSession");

                return new ResponseDTO<string>
                {
                    Result = result.Result,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ResponseDTO<BalanceDto>> insertLibros(LibrosDto libros)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {
                
                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Libros/InsertLibros", libros);

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

       
        
    }
}
