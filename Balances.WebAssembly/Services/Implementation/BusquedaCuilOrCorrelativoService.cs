using Balances.DTO;
using Balances.WebAssembly.Services.Contract;
using System.Net.Http.Json;
using System.Reflection;

namespace Balances.WebAssembly.Services.Implementation
{
    public class BusquedaCuilOrCorrelativoService : IBusquedaCuilOrCorrelativoService
    {

        private readonly HttpClient _httpClient;


        public BusquedaCuilOrCorrelativoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       public async Task<ResponseDTO<BusquedaEntidadResponse>> GetByCuilOrCorrelativo(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseDTO<BusquedaEntidadResponse>>($"BusquedaByCuilOrCorrelativo?cuitcorrelativo={id}");
            }
            catch (HttpRequestException e)
            {
                // Manejar el error apropiadamente
                Console.WriteLine($"Excepción en la solicitud HTTP: {e.Message}");
                return new ResponseDTO<BusquedaEntidadResponse> { };
            }
        }
    }
}

      