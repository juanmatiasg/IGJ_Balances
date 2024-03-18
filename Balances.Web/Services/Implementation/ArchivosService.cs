﻿using Balances.DTO;
using Balances.Web.Services.Implementation;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;


namespace Balances.Web.Services.Contracts
{

    public class ArchivosService : IArchivosService
    {
        private readonly HttpClient _httpClient;

        public ArchivosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                    Message = ex.Message
                };
            }

        }
      

        public async Task<ResponseDTO<BalanceDto>> uploadArchivo(List<FileDTO> files)
        {
            try
            {

                //JsonContent jc = JsonContent.Create(files);
                //var response = await _httpClient.PostAsync("Archivo/InsertArchivos", files);
                var response = await _httpClient.PostAsJsonAsync("Archivo/InsertArchivos", files);

                // Check if the request was successful (status code 2xx)
               // response.EnsureSuccessStatusCode();

                // Deserialize the response
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
                return new ResponseDTO<BalanceDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            };
        }

        private async Task <byte[]> ToByteArrayAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                 await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }


        
        public async Task<ResponseDTO<BalanceDto>> deleteArchivo(FileDTO archivo)
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

