using Balances.DTO;
using Balances.Web.Services.FluentValidation;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using global::Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using Radzen.Blazor;
using System.Security.Cryptography;

namespace Balances.Web.Pages
{
    public partial class Archivos
    {


        private List<string> tipoDeArchivo = new List<string>()
        {
            "Estado Contable",
            "Acta reunión organo",
            "Acta reunión administradores",
            "Informe Fiscalizacion",
            "Otro"
        };


        RadzenGrid<ArchivoDTO> grid;

        [Parameter]
        public string? TipoEntidad { get; set; }

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        public string categoria;



        private List<ArchivoDTO> listArchivo = new List<ArchivoDTO>();
        private ArchivoDTO archivo = new ArchivoDTO();
        private IReadOnlyList<IBrowserFile> selectedFiles = new List<IBrowserFile>();



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
                            TipoEntidad = rsp.Result!.Caratula.Entidad.TipoEntidad;
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
                    if (archivo.Categoria != "Otro")
                    {
                        tipoDeArchivo.Remove(archivo.Categoria);
                    }
                }
            }
        }




        private async Task<ResponseDTO<BalanceDto>> DeleteArchivo(ArchivoDTO archivo)
        {
            var response = new ResponseDTO<BalanceDto>();

            var alerta = await swal.FireAsync(new SweetAlertOptions
            {
                Title = "Esta a punto de borrar la documentación",
                Text = $"¿Desea eliminarlo de la lista?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                CancelButtonText = "Cancelar",
                ConfirmButtonText = "Aceptar"

            });
            if (alerta.IsConfirmed)
            {
                try
                {

                    response = await archivoService.DeleteArchivo(archivo);

                    if (response.IsSuccess)
                    {
                        if (response.IsSuccess)
                        {
                            notificationService.Notify(new NotificationMessage
                            {
                                Severity = NotificationSeverity.Success,
                                Duration = 3000,
                                Summary = "Documentación eliminada correctamente"
                            });
                            listArchivo.Remove(archivo);
                            if(archivo.Categoria != "Otro")
                            {
                            tipoDeArchivo.Add(archivo.Categoria);
                            }

                            await grid.Reload();
                            StateHasChanged();
                        }

                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Duration = 3000,
                        Summary = "No se ha podido eliminar la documentación"
                    });
                }

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




        private void HandleFileUpload(InputFileChangeEventArgs e)
        {

            foreach (var file in e.GetMultipleFiles())
            {

                selectedFiles = e.GetMultipleFiles();

            }
        }

        private async Task<ResponseDTO<BalanceDto>> UploadFile()
        {
            var archivo = new ArchivoDTO();

            var response = new ResponseDTO<BalanceDto>();

            //verificar que adjunto un archivo
            archivo.CantidadArchivos = selectedFiles.Count();

            try
            {
                // var archivo = new ArchivoDTO();
                foreach (var file in selectedFiles)
                {

                    if (file.Size > 0)
                    {
                    
                        var binario = await ToByteArrayAsync(file.OpenReadStream(20 * 1024 * 1024));  // 20 MB

                        archivo.SesionId = sesionId;
                        archivo.Tamaño = binario.Length;
                        archivo.Categoria = categoria;
                        archivo.ContentType = file.ContentType;
                        archivo.NombreArchivo = file.Name;
                        archivo.Hash = Convert.ToHexString(SHA256.HashData(binario));


                        if (archivo.ContentType.Contains("pdf"))
                        {
                            ArchivosValidator archivoValidator = new();
                            ValidationResult result = archivoValidator.Validate(archivo);

                            if (result.IsValid)

                            {
                                listArchivo.Add(archivo);
                                if (archivo.Categoria != "Otro")
                                {
                                    tipoDeArchivo.Remove(archivo.Categoria);
                                }

                                response = await archivoService.UploadArchivo(listArchivo);

                                if (response.IsSuccess)
                                {
                                    notificationService.Notify(new NotificationMessage
                                    {
                                        Severity = NotificationSeverity.Success,
                                        Duration = 3000,
                                        Summary = "Documentación guardada correctamente"
                                    });
                                    await grid.Reload();
                                    StateHasChanged();
                                }
                            }

                        }
                        else
                        {
                            var alerta = await swal.FireAsync(new SweetAlertOptions
                            {
                                Title = "Error en la documentación",
                                Text = $"Solo se aceptan archivos en formato PDF",
                                Icon = SweetAlertIcon.Warning,
                                ConfirmButtonText = "Aceptar"

                            });
                        }

                    }
                }

            }

            catch (Exception ex)
            {
                response.Message = $"An error occurred while uploading files: {ex.Message}";
                notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Duration = 3000,
                    Summary = "No se ha podido guardar la documentacion"
                });
            }


            return response;
        }







    }
}