using Balances.DTO;
using Balances.Web.Services.Implementation;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Balances.Web.Services.Contracts
{
    public class EstadoContableService : IEstadoContableClientService
    {
        private readonly HttpClient _httpClient;

        public EstadoContableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<ResponseDTO<BalanceDto>> insertEECC(EstadoContableDto estadoContableDto)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("EstadoContable/InsertEECC", estadoContableDto);

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

        public async Task<ResponseDTO<BalanceDto>> insertRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto)
        {
            ResponseDTO<BalanceDto> rsp = new();
            rsp.IsSuccess = false;
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var respuesta = await _httpClient.PostAsJsonAsync("EstadoContable/InsertRubro", rubroPatrimonioNetoDto);

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
        public async Task<ResponseDTO<BalanceDto>> deleteRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto)
        {
            ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
            rsp.IsSuccess = false;

            try
            {
                // Crear una solicitud DELETE y adjuntar el objeto autoridad en el cuerpo (si es necesario)
                var request = new HttpRequestMessage(HttpMethod.Delete, $"EstadoContable/DeleteRubro");
                request.Content = new StringContent(JsonConvert.SerializeObject(rubroPatrimonioNetoDto), Encoding.UTF8, "application/json");

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
