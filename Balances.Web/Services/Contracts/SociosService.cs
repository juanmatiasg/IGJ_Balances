using Balances.DTO;
using Balances.Model;
using Balances.Web.Services.Implementation;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Balances.Web.Services.Contracts
{
    public class SociosService : ISociosService
    {
        private readonly HttpClient _httpClient;

        public SociosService(HttpClient httpClient)
        {
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

        public async Task<ResponseDTO<BalanceDto>> insertPersonaHumana(PersonaHumanaDto personaHumana)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Socios/InsertPersonaHumana", personaHumana);

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
        public async Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumana)
        {
            ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
            rsp.IsSuccess = false;

            try
            {
                // Crear una solicitud DELETE y adjuntar el objeto autoridad en el cuerpo (si es necesario)
                var request = new HttpRequestMessage(HttpMethod.Delete, $"Socios/DeletePersonaHumana");
                request.Content = new StringContent(JsonConvert.SerializeObject(personaHumana), Encoding.UTF8, "application/json");

                // Enviar la solicitud DELETE directamente con SendAsync
                var respuesta = await _httpClient.SendAsync(request);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer la respuesta JSON y deserializarla a ResponseDTO<BalanceDto>
                    var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                    if (result.IsSuccess)
                    {
                        rsp = result;

                    }
                    else {
                        rsp.Message = $"SocioService.deletePersonaHumana. {result.Message}";
                    }
                  
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    rsp.Message = $"Error de comunicación. Código de estado: {respuesta.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                rsp.Message = ex.Message;
            }

            return rsp;
        }


        public async Task<ResponseDTO<BalanceDto>> insertPersonaJuridica(PersonaJuridicaDto personaJuridica)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Socios/InsertPersonaJuridica", personaJuridica);

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
        public async Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridica)
        {
            ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
            rsp.IsSuccess = false;

            try
            {
                // Crear una solicitud DELETE y adjuntar el objeto autoridad en el cuerpo (si es necesario)
                var request = new HttpRequestMessage(HttpMethod.Delete, $"Socios/DeletePersonaJuridica");
                request.Content = new StringContent(JsonConvert.SerializeObject(personaJuridica), Encoding.UTF8, "application/json");

                // Enviar la solicitud DELETE directamente con SendAsync
                var respuesta = await _httpClient.SendAsync(request);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer la respuesta JSON y deserializarla a ResponseDTO<BalanceDto>
                    var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                    if (result.IsSuccess)
                    {
                        rsp = result;

                    }
                    else
                    {
                        rsp.Message = $"SocioService.deletePersonaHumana. {result.Message}";
                    }
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


       
    }
}
