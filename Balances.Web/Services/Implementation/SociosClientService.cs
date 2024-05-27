using Balances.DTO;
using Balances.Web.Services.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;

namespace Balances.Web.Services.Contracts
{
    public class SociosClientService : ISociosClientService
    {
        private readonly HttpClient _httpClient;

        public SociosClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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


                rsp = result!;
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
                    if (result!.IsSuccess)
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
                        rsp.Message = $"SocioService.deletePersonaJuridica. {result.Message}";
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

        public  async Task<List<JObject>> GetAllCountries()
        {

            string url = "https://restcountries.com/v3.1/all";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(json);
                List<JObject> countries = new List<JObject>();

                foreach (JObject item in jsonArray)
                {
                    countries.Add(item);
                }

                return countries;
            }
            else
            {
                throw new Exception($"Failed to retrieve countries. Status code: {response.StatusCode}");
            }

        }


        public async Task<List<string>> GetAllProvince()
        {
            List<string> provinceNames = new List<string>();

            string url = "https://apis.datos.gob.ar/georef/api/provincias";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);
                JArray provinciasArray = (JArray)jsonObject["provincias"];

                foreach (JObject provincia in provinciasArray)
                {
                    string nombre = provincia["nombre"].ToString();
                    provinceNames.Add(nombre);
                }

                return provinceNames;
            }
            else
            {
                throw new Exception($"Failed to retrieve provinces. Status code: {response.StatusCode}");
            }
        }
    }
}
