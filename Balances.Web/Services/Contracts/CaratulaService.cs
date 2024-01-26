using Balances.DTO;
using Balances.Model;
using Balances.Web.Pages;
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
            catch(Exception ex) {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }
        public async Task<ResponseDTO<string>> loadCaratula(string id)
        {
            try
            {
                //*probamos si existe el balance id */
                var rsp = await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

                //si encontro balance
                if (rsp.Result != null)
                {
                    // Enviar la solicitud POST directamente con PostAsJsonAsync
                    var response = await _httpClient.PostAsJsonAsync($"Session/{id}",id);

                    // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                    //var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();

                    return new ResponseDTO<string>
                    {
                        Result = id,
                        IsSuccess = true,
                        Message = "FUNCIONO OK"
                    };
                }
                else
                {
                    return new ResponseDTO<string>
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = "No pudo Cargar la Caratula"
                    };
                }

              
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "No pudo Cargar la Caratula"
                };
            }

             
         
        }

        public async Task<ResponseDTO<BalanceDto>> initTramite(CaratulaDto caratula)
        {
            try
            { 
                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Caratula/InsertCaratula", caratula);

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

