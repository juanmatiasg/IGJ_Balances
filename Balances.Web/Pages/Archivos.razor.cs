using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Balances.Web;
using Balances.Web.Shared;
using Blazorise;
using Radzen;
using Radzen.Blazor;
using Balances.DTO;
using Balances.Model;
using Balances.Utilities;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using Microsoft.AspNetCore.Http;
using BlazorInputFileExtended;
using Microsoft.AspNetCore.Http.Internal;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;

namespace Balances.Web.Pages
{
    public partial class Archivos
    {
        RadzenUpload upload;
        RadzenUpload uploadDD;
        int progress;
        bool showProgress;
        bool showComplete;
        string completionMessage;
        bool cancelUpload = false;
        [Parameter]
        public string? TipoEntidad { get; set; }

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        private bool isLoading = false;
        private double progressPercentage = 0.0;
        private string categoria = "";
        private string msgError = "";
        private string msgErrorTipoArchivo = "";
        private IReadOnlyList<IBrowserFile> selectedFiles = new List<IBrowserFile>();
        private List<ArchivoDTO> listArchivo = new List<ArchivoDTO>();
        private bool isUploaded = false;
        protected override async void OnInitialized()
        {
            await Load();
            base.OnInitialized();
        }

        private async Task Load()
        {
            ResponseDTO<BalanceDto> rsp = new();
            sesionId = await sessionStorage.GetItemAsync<string>("SessionId");
            try
            {
                if (sesionId == null)
                {
                    var sesionRespuesta = await sesionService.getNewSession();
                    sesionId = sesionRespuesta.Result;
                    sessionStorage.SetItemAsync("SessionId", sesionId);
                }
                else
                {
                    var rst = await sesionService.getBalanceId(sesionId);
                    if (rst is not null)
                    {
                        balid = rst;
                        rsp = await balanceService.getBalance(balid);
                        if (rsp.IsSuccess)
                        {
                            TipoEntidad = rsp.Result.Caratula.Entidad.TipoEntidad;
                            var rspArchivos = rsp.Result.Archivos;
                            setListArchivos(rspArchivos);
                            StateHasChanged();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}");
            }
        }

        private void setListArchivos(List<ArchivoDTO> list)
        {
            if (list != null)
            {
                foreach (var x in list)
                {
                    var archivo = new ArchivoDTO
                    {
                        Id = x.Id,
                        SesionId = sesionId,
                        NombreArchivo = x.NombreArchivo,
                        Categoria = x.Categoria,
                        Hash = x.Hash,
                        Tamaño = x.Tamaño,
                        FechaCreacion = x.FechaCreacion
                    };
                    this.listArchivo.Add(archivo);
                }
            }
        }

        private string conversionesDeArchivos(long file)
        {
            // Tamaño del archivo en bytes (esto podría provenir de tu archivo subido)
            long fileSizeInBytes = file;
            if (fileSizeInBytes < 1024)
            {
                return $"{Math.Round((double)fileSizeInBytes)} Bytes";
            }
            else if (fileSizeInBytes < 1024 * 1024)
            {
                double fileSizeInKB = (double)fileSizeInBytes / 1024;
                return $"{Math.Round(fileSizeInKB)} KB";
            }
            else if (fileSizeInBytes < 1024L * 1024 * 1024)
            {
                double fileSizeInMB = (double)fileSizeInBytes / (1024 * 1024);
                return $"{Math.Round(fileSizeInMB)} MB";
            }
            else if (fileSizeInBytes < 1024L * 1024 * 1024 * 1024)
            {
                double fileSizeInGB = (double)fileSizeInBytes / (1024 * 1024 * 1024);
                return $"{Math.Round(fileSizeInGB)} GB";
            }
            else
            {
                double fileSizeInTB = (double)fileSizeInBytes / (1024L * 1024 * 1024 * 1024);
                return $"{Math.Round(fileSizeInTB)} TB";
            }
        }

        private void HandleFileUpload(InputFileChangeEventArgs e)
        {
            try
            {
                isLoading = true; // Mostrar indicador de carga
                foreach (var file in e.GetMultipleFiles())
                {
                    msgError = "";
                    if (Path.GetExtension(file.Name).ToLower() == ".pdf")
                    {
                        if (file.Size <= 20 * 1024 * 1024) // 20 MB en bytes
                        {
                            msgError = "";
                            selectedFiles = e.GetMultipleFiles();
                        }
                        else
                        {
                            msgError = $"El archivo {file.Name} excede el tamaño máximo permitido de 20 MB.";
                        }
                    }
                    else
                    {
                        msgError = $"El archivo {file.Name} no es un archivo PDF.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: HandleFileUpload {ex.Message}");
            }
            finally
            {
                isLoading = false; // Ocultar indicador de carga
                isUploaded = false;
            }
        }

        private bool checkDataFile()
        {
            if (selectedFiles.Count < 0)
            {
                msgError = "No seleccionaste ningún archivo";
                return false;
            }

            msgError = "";
            return true;
        }

        private async Task<ResponseDTO<BalanceDto>> UploadFile()
        {
            var response = new ResponseDTO<BalanceDto>();
            try
            {
                isLoading = true;
                isUploaded = true;
                if (checkData())
                {
                    var archivo = new ArchivoDTO();
                    foreach (var file in selectedFiles)
                    {
                        if (file.Size > 0)
                        {
                            var binario = await ToByteArrayAsync(file.OpenReadStream(20 * 1024 * 1024)); // 20 MB
                            archivo.SesionId = sesionId;
                            archivo.Tamaño = binario.Length;
                            archivo.ContentType = file.ContentType;
                            archivo.Categoria = categoria;
                            archivo.NombreArchivo = file.Name;
                            archivo.Hash = Convert.ToHexString(SHA256.HashData(binario));
                            // Calcular el progreso de la carga
                            double totalFileSize = selectedFiles.Sum(file => file.Size);
                            double uploadedSize = 0.0;
                            double stepSize = totalFileSize / 5; // Dividir en 5 pasos (20, 40, 60, 80, 100)
                            for (int i = 1; i <= 5; i++)
                            {
                                uploadedSize += stepSize;
                                progressPercentage = (uploadedSize / totalFileSize) * 100;
                                await Task.Delay(1000); // Esperar 1 segundo en cada paso (simulando una carga más lenta)
                            }

                            listArchivo.Add(archivo);
                            response = await archivoService.uploadArchivo(listArchivo);
                            if (response.IsSuccess)
                            {
                                StateHasChanged();
                            }
                            else
                            {
                                response.Message = $"Error uploading files";
                            }
                        }
                        else
                        {
                            msgError = $"El archivo {file.Name} está vacío. Por favor, seleccione un archivo válido.";
                        }
                    }
                }
                else
                {
                    response.Message = $"El campo categoria no puede estar vacio";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred while uploading files: {ex.Message}";
            }
            finally
            {
                isLoading = false;
                isUploaded = false;
            }

            return response;
        }

        private bool checkData()
        {
            if (!string.IsNullOrEmpty(categoria))
            {
                msgErrorTipoArchivo = "";
            }
            else
            {
                msgErrorTipoArchivo = "El campo no puede estar vacio";
                return false;
            }

            msgErrorTipoArchivo = "";
            return true;
        }

        private async Task<ResponseDTO<BalanceDto>> deleteArchivo(ArchivoDTO archivo)
        {
            var response = new ResponseDTO<BalanceDto>();
            try
            {
                response = await archivoService.deleteArchivo(archivo);
                if (response.IsSuccess)
                {
                    listArchivo.Remove(archivo);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<byte[]> ToByteArrayAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        void CompleteUpload(UploadCompleteEventArgs args)
        {
            if (!args.Cancelled)
                completionMessage = "Upload Complete!";
            else
                completionMessage = "Upload Cancelled!";
            showProgress = false;
            showComplete = true;
        }

        void TrackProgress(UploadProgressArgs args)
        {
            showProgress = true;
            showComplete = false;
            progress = args.Progress;
            // cancel upload
            args.Cancel = cancelUpload;
            // reset cancel flag
            cancelUpload = false;
        }

        void CancelUpload()
        {
            cancelUpload = true;
        }

        int customParameter = 1;
        void OnChange(UploadChangeEventArgs args, string name)
        {
            foreach (var file in args.Files)
            {
                Console.WriteLine($"File: {file.Name} / {file.Size} bytes");
            }

            Console.WriteLine($"{name} changed");
        }

        void OnProgress(UploadProgressArgs args, string name)
        {
            Console.WriteLine($"{args.Progress}% '{name}' / {args.Loaded} of {args.Total} bytes.");
            if (args.Progress == 100)
            {
                foreach (var file in args.Files)
                {
                    Console.WriteLine($"Uploaded: {file.Name} / {file.Size} bytes");
                }
            }
        }

        void OnComplete(UploadCompleteEventArgs args)
        {
            Console.WriteLine($"Server response: {args.RawResponse}");
        }

        void OnClientChange(UploadChangeEventArgs args)
        {
            Console.WriteLine($"Client-side upload changed");
            foreach (var file in args.Files)
            {
                Console.WriteLine($"File: {file.Name} / {file.Size} bytes");
                try
                {
                    long maxFileSize = 10 * 1024 * 1024;
                    // read file
                    var stream = file.OpenReadStream(maxFileSize);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Client-side file read error: {ex.Message}");
                }
            }
        }
    }
}