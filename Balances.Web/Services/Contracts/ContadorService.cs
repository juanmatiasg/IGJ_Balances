using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Balances.Web.Services.Contracts
{
    public class ContadorService : IContadorService
    {
        private readonly HttpClient _httpClient;

        public ContadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");
        }

        public async Task<ResponseDTO<ContadorDto>> getContador(string idBalance)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<ContadorDto>>($"Contador/{idBalance}");

        }

        public async Task<ResponseDTO<ContadorDto>> postContador(string id,string nombre,string tipoDoc,string nroDoc,string nroFiscal,string tomo,string folio,DateTime fechaInformeAuditorExt,string nroLegalInfoAudExt)
        {
            try
            {
                var contador = new ContadorDto
                {
                    id = id,
                    Nombre = nombre,
                    TipoDocumento = tipoDoc ,
                    NroDocumento = nroDoc,
                    NroFiscal = nroFiscal,
                    Tomo= tomo,
                    Folio= folio,
                    FechaInformeAuditorExt =fechaInformeAuditorExt,
                    NroLegalInfoAudExt= nroLegalInfoAudExt,
                };
               


                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Contador/Insert", contador);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ContadorDto>>();
                
                return new ResponseDTO<ContadorDto>
                {
                    Result = result.Result,
                    IsSuccess = true,
                    Message = "Contador insertado correctamente"
                };

              
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return new ResponseDTO<ContadorDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Error in the request"
                };
            }
        }

        public async Task<ResponseDTO<string>> setSession(string idBalance)
        {
            try
            {
                var session = new ResponseDTO<string>
                {
                  Result = idBalance,
                  Message ="Session Created",
                  IsSuccess=true,
                };



                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync($"Session/{idBalance}",session);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<string>>();

                return new ResponseDTO<string>
                {
                    Result = result.Result,
                    IsSuccess = true,
                    Message = result.Message
                };


            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Error in the request"
                };
            }
        }
    }
}
