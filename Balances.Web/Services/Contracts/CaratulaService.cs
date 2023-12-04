using Balances.DTO;
using Balances.Model;
using Balances.Web.Services.Implementation;

using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

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
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BusquedaEntidadResponse>>($"BusquedaByCuilOrCorrelativo?nroCorrelativo={nroCorrelativo}");
        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

        }

        public async Task<ResponseDTO<BalanceDto>> initTramite(string email, DateTime fechaInicio, DateTime fechaDeCierre, string razonSocial, string tipoEntidad, string domicilio, bool sedeSocialInscripta, string nroCorrelativo)
        {
            try
            {
                var caratulaDto = new CaratulaDto
                {
                    Email = email,
                    FechaInicio = fechaInicio,
                    FechaDeCierre = fechaDeCierre,
                    Entidad = new Entidad
                    {
                        RazonSocial = razonSocial,
                        TipoEntidad = tipoEntidad,
                        Domicilio = domicilio,
                        SedeSocialInscripta = sedeSocialInscripta,
                        Correlativo = nroCorrelativo
                    }
                };



                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Caratula/InsertCaratula", caratulaDto);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                
                return new ResponseDTO<BalanceDto>
                {
                    Result = result.Result,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };

              
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Error in the request"
                };
            }
        }

      

      
    }

}

