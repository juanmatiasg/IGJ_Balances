using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class CaratulaService : ICaratulaService
    {
        private readonly HttpClient _httpClient;

        public CaratulaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7172/"); // Replace with your actual API base URL

        } 


        public async Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo)
        {
            var requestUrl = $"BusquedaByCuilOrCorrelativo?cuitcorrelativo={nroCorrelativo}";
            try
        {
        var response = await _httpClient.GetFromJsonAsync<ResponseDTO<BusquedaEntidadResponse>>(requestUrl);

        // Check if the response indicates success (you can customize this check based on your API's behavior)
        if (response != null && response.IsSuccess)
        {
            return response;
        }
        else
        {
            // Handle the error condition, throw an exception, or return a custom error response
            throw new HttpRequestException($"HTTP request failed with status code {response?.Message}");
        }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            throw; // Rethrow the exception for higher-level handling
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
            }
        }
}
