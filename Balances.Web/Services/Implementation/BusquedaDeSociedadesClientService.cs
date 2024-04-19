using Balances.DTO;
using Balances.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class BusquedaDesociedadesClientService : IBusquedaDeSociedadesClientService
    {
        private readonly HttpClient _httpClient;

        public BusquedaDesociedadesClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<BusquedaEntidadResponse>> findSociedad(string nroCorrelativo)
        {
            var sociedad = await _httpClient.GetFromJsonAsync<ResponseDTO<BusquedaEntidadResponse>>($"BusquedaByCuilOrCorrelativo?nroCorrelativo={nroCorrelativo}");

            return sociedad;
        }
    }
}
