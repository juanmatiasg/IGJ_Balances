using Balances.DTO;
using Balances.Web.Services.FluentValidation;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class ContadorService : IContadorClientService
    {
        private readonly HttpClient _httpClient;



        public ContadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;


        }



        public async Task<ResponseDTO<BalanceDto>> getContador(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Contador/{id}");

        }



        public async Task<ResponseDTO<BalanceDto>> postContador(ContadorDto contador)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            ContadorValidator contadorValidator = new();
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


    }
}
