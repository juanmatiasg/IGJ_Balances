using Balances.DTO;
using Balances.Web.Services.Implementation;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Balances.Web.Services.Contracts
{
    public class AutoridadClientService : IAutoridadClientService
    {
        private readonly HttpClient _httpClient;

        public AutoridadClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<BalanceDto>> insertAutoridad(AutoridadDto autoridad)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Autoridades/Insert", autoridad);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<AutoridadesDTO>
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


        public async Task<ResponseDTO<BalanceDto>> deleteAutoridad(AutoridadDto autoridad)
        {
            ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
            rsp.IsSuccess = false;

            try
            {
                // Crear una solicitud DELETE y adjuntar el objeto autoridad en el cuerpo (si es necesario)
                var request = new HttpRequestMessage(HttpMethod.Delete, $"Autoridades/DeleteAutoridad");
                request.Content = new StringContent(JsonConvert.SerializeObject(autoridad), Encoding.UTF8, "application/json");

                // Enviar la solicitud DELETE directamente con SendAsync
                var respuesta = await _httpClient.SendAsync(request);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer la respuesta JSON y deserializarla a ResponseDTO<BalanceDto>
                    var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                    rsp = result;
                    rsp.IsSuccess = true;
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    rsp.Message = $"Error en la solicitud DELETE. Código de estado: {respuesta.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                rsp.Message = ex.Message;
            }

            return rsp;
        }

        public async Task<ResponseDTO<BalanceDto>> updateAutoridad(AutoridadDto autoridad)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Autoridades/Update", autoridad);

                // Leer la respuesta JSON y deserializarla
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
