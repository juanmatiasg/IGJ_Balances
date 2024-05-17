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
using Microsoft.Win32;
using Blazorise.Extensions;
using System.Net.Mime;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Runtime.Intrinsics.Arm;
using FileInfo = System.IO.FileInfo;

namespace Balances.Web.Pages
{
    public partial class Archivos
    {


        private string[] tipoDeArchivo =
        {
            "Estado Contable",
            "Acta reunión organo",
            "Acta reunión administradores",
            "Informe Fiscalizacion",
            "Otro"
        };

        private string? categoria;

        RadzenUpload uploadDD;

        RadzenGrid<ArchivoDTO> grid;

        [Parameter]
        public string? TipoEntidad { get; set; }

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }


        private string msgError = "";
        private string msgErrorTipoArchivo = "";

        private List<ArchivoDTO> listArchivo = new List<ArchivoDTO>();
        private ArchivoDTO archivo = new ArchivoDTO();


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

                    sesionId = sesionRespuesta.Result!;

                    await sessionStorage.SetItemAsync("SessionId", sesionId);
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
                            await grid.Reload();
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



        private bool checkData()
        {
            if (archivo.Categoria.IsNullOrEmpty())
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

        private async Task<ResponseDTO<BalanceDto>> DeleteArchivo(ArchivoDTO archivo)
        {
            var response = new ResponseDTO<BalanceDto>();
            try
            {

                response = await archivoService.DeleteArchivo(archivo);

                if (response.IsSuccess)
                {
                    listArchivo.Remove(archivo);

                    await grid.Reload();
                    StateHasChanged();
                }

                else
                {
                    response.Message = response.Message;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }


            return response;
        }

        private async void OnProgress(UploadProgressArgs args)
        {
           
            if (checkData())
            {
                if (args.Progress == 100)
                {

               
                    foreach (var file in args.Files)
                    {
                   
                        ArchivoDTO archivo = new ArchivoDTO();                                
                        

                        archivo.SesionId = sesionId;
                        archivo.Tamaño = file.Size;
                        archivo.ContentType = "pdf";
                        archivo.Categoria = categoria;
                        archivo.NombreArchivo = file.Name;                      
                        

                        listArchivo.Add(archivo);

                    }
                    await UploadFiles(listArchivo);
                }
            }
        }


        private async Task<byte[]> ToByteArrayAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }






        private async Task<ResponseDTO<BalanceDto>> UploadFiles(List<ArchivoDTO> archivos)
        {

            ResponseDTO<BalanceDto> response = new ResponseDTO<BalanceDto>();

            response = await archivoService.UploadArchivo(archivos);


            if (response.IsSuccess)
            {
                await grid.Reload();
                StateHasChanged();
            }
            else
            {
                response.Message = $"Error uploading files";
            }


            return response;

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

        private void OnChange(UploadChangeEventArgs args) {
            foreach (var file in args.Files)
            {
                using var  stream = file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024);
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
               
              
            }
        }
        
    }
}