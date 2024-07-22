using Balances.DTO;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class CaratulaClientService : ICaratulaClientService
    {
        private readonly HttpClient _httpClient;


        public CaratulaClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }




        public async Task<ResponseDTO<BalanceDto>> loadCaratula(string id)
        {
            var responseDto = new ResponseDTO<BalanceDto>();

            try
            {

                var rsp = await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

                if (rsp.Result != null)
                {

                    responseDto.Result = rsp.Result;
                    responseDto.IsSuccess = rsp.IsSuccess;
                    responseDto.Message = rsp.Message;
                }

            }
            catch
            {
                throw;

            }
            return responseDto;

        }

        public async Task<ResponseDTO<BalanceDto>> insertCaratula(CaratulaDto caratula)
        {
            var responseDto = new ResponseDTO<BalanceDto>();
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Caratula/InsertCaratula", caratula);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();





                responseDto.Result = result.Result;
                responseDto.IsSuccess = result.IsSuccess;
                responseDto.Message = result.Message;



            }
            catch
            {
                throw;
            }

            return responseDto;
        }

        public async Task<ResponseDTO<BalanceDto>> rectificarBalance(BalanceDto balance)
        {
            var responseDto = new ResponseDTO<BalanceDto>();
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Caratula/RectificarCaratula", balance);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();





                responseDto.Result = result.Result;
                responseDto.IsSuccess = result.IsSuccess;
                responseDto.Message = result.Message;



            }
            catch
            {
                throw;
            }

            return responseDto;
        }

        public async Task<ResponseDTO<BalanceDto>> updateCaratula(CaratulaDto caratula)
        {
            var responseDto = new ResponseDTO<BalanceDto>();
            try
            {

                // Enviar la solicitud POST directamente con PostAsJsonAsync
                var response = await _httpClient.PostAsJsonAsync("Caratula/UpdateCaratula", caratula);

                // Leer la respuesta JSON y deserializarla a ResponseDTO<CaratulaDto>
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();





                responseDto.Result = result.Result;
                responseDto.IsSuccess = result.IsSuccess;
                responseDto.Message = result.Message;



            }
            catch
            {
                throw;
            }

            return responseDto;
        }

    }

}

