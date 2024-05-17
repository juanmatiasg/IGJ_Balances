using Balances.DTO;
using Balances.Web.Services.Implementation;
using Newtonsoft.Json;
using OneOf.Types;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;


namespace Balances.Web.Services.Contracts
{

    public class ArchivosClientService : IArchivosClientService
    {
        private readonly HttpClient _httpClient;

        public ArchivosClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<ResponseDTO<BalanceDto>> UploadArchivo(List<ArchivoDTO> files)
        {
        
            try
            { 

                using var response = await _httpClient.PostAsJsonAsync("Archivo/InsertArchivos", files);

                response.EnsureSuccessStatusCode(); // Lanzará una excepción si la solicitud no tiene éxito (código de estado diferente de 2xx)

                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();

                if (result!.IsSuccess)
                {
                    return new ResponseDTO<BalanceDto>
                    {
                        Result = result.Result,
                        IsSuccess = true,
                        Message = "Upload successful"
                    };
                }
                else
                {
                    return new ResponseDTO<BalanceDto>
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = result.Message ?? "Error: Response message is empty"
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                // Si hay un error de comunicación (por ejemplo, problemas de red), capturamos la excepción y devolvemos un mensaje de error
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // Capturamos cualquier otra excepción y devolvemos un mensaje de error
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

      

        public async Task<ResponseDTO<BalanceDto>> DeleteArchivo(ArchivoDTO archivo)
        {
            ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
            rsp.IsSuccess = false;

            try
            {
            

                // Crear una solicitud DELETE y adjuntar el objeto autoridad en el cuerpo (si es necesario)
                var request = new HttpRequestMessage(HttpMethod.Delete, $"Archivo/DeleteArchivo");
                request.Content = new StringContent(JsonConvert.SerializeObject(archivo), Encoding.UTF8, "application/json");

                // Enviar la solicitud DELETE directamente con SendAsync
                var respuesta = await _httpClient.SendAsync(request);

                // Verificar si la solicitud fue exitosa (código 2xx)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer la respuesta JSON y deserializarla a ResponseDTO<BalanceDto>
                    var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                    rsp = result;
                    rsp.IsSuccess = true;
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

        
      
    }


   





}

