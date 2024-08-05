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
        public async Task<ResponseDTO<BalanceDto>> updatePersonaHumana(PersonaHumanaDto personaHumana)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Socios/UpdatePersonaHumana", personaHumana);

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
        public async Task<ResponseDTO<BalanceDto>> updatePersonaJuridica(PersonaJuridicaDto personaJuridica)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("Socios/UpdatePersonaJuridica", personaJuridica);

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


        public async Task<List<string>> GetAllCountries()
        {
            try
            {

                List<string> countries = new List<string>
        {
            "Afghanistan",
            "Albania",
            "Algeria",
            "Andorra",
            "Angola",
            "Antigua and Barbuda",
            "Argentina",
            "Armenia",
            "Australia",
            "Austria",
            "Azerbaijan",
            "Bahamas",
            "Bahrain",
            "Bangladesh",
            "Barbados",
            "Belarus",
            "Belgium",
            "Belize",
            "Benin",
            "Bhutan",
            "Bolivia",
            "Bosnia and Herzegovina",
            "Botswana",
            "Brazil",
            "Brunei",
            "Bulgaria",
            "Burkina Faso",
            "Burundi",
            "Cabo Verde",
            "Cambodia",
            "Cameroon",
            "Canada",
            "Central African Republic",
            "Chad",
            "Chile",
            "China",
            "Colombia",
            "Comoros",
            "Congo",
            "Costa Rica",
            "Croatia",
            "Cuba",
            "Cyprus",
            "Czech Republic",
            "Denmark",
            "Djibouti",
            "Dominica",
            "Dominican Republic",
            "Ecuador",
            "Egypt",
            "El Salvador",
            "Equatorial Guinea",
            "Eritrea",
            "Estonia",
            "Eswatini",
            "Ethiopia",
            "Fiji",
            "Finland",
            "France",
            "Gabon",
            "Gambia",
            "Georgia",
            "Germany",
            "Ghana",
            "Greece",
            "Grenada",
            "Guatemala",
            "Guinea",
            "Guinea-Bissau",
            "Guyana",
            "Haiti",
            "Honduras",
            "Hungary",
            "Iceland",
            "India",
            "Indonesia",
            "Iran",
            "Iraq",
            "Ireland",
            "Israel",
            "Italy",
            "Jamaica",
            "Japan",
            "Jordan",
            "Kazakhstan",
            "Kenya",
            "Kiribati",
            "Korea, North",
            "Korea, South",
            "Kosovo",
            "Kuwait",
            "Kyrgyzstan",
            "Laos",
            "Latvia",
            "Lebanon",
            "Lesotho",
            "Liberia",
            "Libya",
            "Liechtenstein",
            "Lithuania",
            "Luxembourg",
            "Madagascar",
            "Malawi",
            "Malaysia",
            "Maldives",
            "Mali",
            "Malta",
            "Marshall Islands",
            "Mauritania",
            "Mauritius",
            "Mexico",
            "Micronesia",
            "Moldova",
            "Monaco",
            "Mongolia",
            "Montenegro",
            "Morocco",
            "Mozambique",
            "Myanmar",
            "Namibia",
            "Nauru",
            "Nepal",
            "Netherlands",
            "New Zealand",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "North Macedonia",
            "Norway",
            "Oman",
            "Pakistan",
            "Palau",
            "Palestine",
            "Panama",
            "Papua New Guinea",
            "Paraguay",
            "Peru",
            "Philippines",
            "Poland",
            "Portugal",
            "Qatar",
            "Romania",
            "Russia",
            "Rwanda",
            "Saint Kitts and Nevis",
            "Saint Lucia",
            "Saint Vincent and the Grenadines",
            "Samoa",
            "San Marino",
            "Sao Tome and Principe",
            "Saudi Arabia",
            "Senegal",
            "Serbia",
            "Seychelles",
            "Sierra Leone",
            "Singapore",
            "Slovakia",
            "Slovenia",
            "Solomon Islands",
            "Somalia",
            "South Africa",
            "South Sudan",
            "Spain",
            "Sri Lanka",
            "Sudan",
            "Suriname",
            "Sweden",
            "Switzerland",
            "Syria",
            "Taiwan",
            "Tajikistan",
            "Tanzania",
            "Thailand",
            "Timor-Leste",
            "Togo",
            "Tonga",
            "Trinidad and Tobago",
            "Tunisia",
            "Turkey",
            "Turkmenistan",
            "Tuvalu",
            "Uganda",
            "Ukraine",
            "United Arab Emirates",
            "United Kingdom",
            "United States",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Vatican City",
            "Venezuela",
            "Vietnam",
            "Yemen",
            "Zambia",
            "Zimbabwe"
        };

                return countries;
            }
            catch
            {

                throw new Exception($"Failed to retrieve countries.");
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
