using Balances.DTO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class CaratulaClientService : ICaratulaClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CaratulaClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

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
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"{"Error:GetSession" + ex.Message}"
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
                    //var response = await _httpClient.PostAsJsonAsync($"Session/{id}", id);

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

                await _httpClient.PostAsJsonAsync($"Session/{result.Result.Id}", result.Result.Id);


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

        public async Task<ResponseDTO<string>> setSession(string id)
        {
            try
            {
                var idKey = _httpContextAccessor.HttpContext.Session.Id;

                Console.WriteLine("Prueba" + idKey);

                var response = await _httpClient.PostAsJsonAsync($"Session/{id}", id);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<string>>();

                if (result.IsSuccess)
                {
                    return new ResponseDTO<string>
                    {
                        Result = result.Result,
                        Message = result.Message,
                        IsSuccess = result.IsSuccess
                    };
                }
                else
                {
                    return new ResponseDTO<string>
                    {
                        Result = result.Result,
                        Message = result.Message,
                        IsSuccess = result.IsSuccess
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    Message = $"{"Error:SetSession" + ex.Message}",
                    IsSuccess = false
                };
            }

        }
    }

}

