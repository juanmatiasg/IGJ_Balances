using Balances.DTO;
using Balances.Model;
using Balances.Web.Services.Implementation;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using javax.jws;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class CaratulaService : ICaratulaService
    {
        private readonly HttpClient _httpClient;

        public CaratulaService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo)
        {
            try
            {

                var response = await _httpClient.GetFromJsonAsync<ResponseDTO<BusquedaEntidadResponse>>($"BusquedaByCuilOrCorrelativo?cuitOrCorrelativo={nroCorrelativo}");


                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                return new ResponseDTO<BusquedaEntidadResponse>
                {
                    IsSuccess = false,
                    Message = "Error en la solicitud HTTP"
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
                return new ResponseDTO<BusquedaEntidadResponse>
                {
                    IsSuccess = false,
                    Message = "Error de deserialización JSON"
                };
            }
        }
    }

}

