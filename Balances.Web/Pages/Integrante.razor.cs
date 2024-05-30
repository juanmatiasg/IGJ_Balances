using Balances.DTO;
using Balances.Web.Services.FluentValidation;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using global::Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace Balances.Web.Pages
{
    public partial class Integrante
    {
        [Parameter]
        public string? TipoEntidad { get; set; }

        RadzenGrid<PersonaHumanaDto> grid;


        private PersonaHumanaDto model = new PersonaHumanaDto();
        private List<PersonaHumanaDto> listPersonaHumana = new List<PersonaHumanaDto>();


        private string[] tiposDocumentos =
       {
            "DNI",
            "Pasaporte"
        };

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            ResponseDTO<BalanceDto> rsp = new();
            try
            {
                sesionId = await sessionStorage.GetItemAsync<string>("SessionId");

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
                            resultPersonaHumana(rsp.Result.Socios.PersonasHumanas);
                            await grid.Reload();
                            StateHasChanged();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rsp.Message = $"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}";
            }
        }

        private async Task<ResponseDTO<BalanceDto>> InsertPersonaHumana()
        {
            ResponseDTO<BalanceDto> respuesta = new();
            try
            {
                model.SesionId = sesionId;

                PersonaHumanaValidator personaHumanaValidator = new();
                ValidationResult result = personaHumanaValidator.Validate(model);

                if (result.IsValid)
                {
                    //listPersonaHumana.Add(modelPersonaHumana);
                    respuesta = await socioService.insertPersonaHumana(model);

                    if (respuesta.IsSuccess)
                    {

                        notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Success,
                            Duration = 3000,
                            Summary = "Datos guardados correctamente"
                        });
                        resultPersonaHumana(respuesta.Result!.Socios.PersonasHumanas);

                        cleanInputsHumana();
                        await grid.Reload();
                        StateHasChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Duration = 3000,
                    Summary = "No se ha podido guardar los datos"
                });
            }

            return respuesta;

        }

        private void cleanInputsHumana()
        {
            model = new PersonaHumanaDto();
        }

        private void resultPersonaHumana(List<PersonaHumanaDto> listPersonaHumana)
        {
            this.listPersonaHumana = listPersonaHumana;
        }

        private async Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumanaDto)
        {
            var respuesta = new ResponseDTO<BalanceDto>();

            var alerta = await swal.FireAsync(new SweetAlertOptions
            {
                Title = "Esta a punto de borrar al integrante",
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
                    personaHumanaDto.SesionId = sesionId;

                    respuesta = await socioService.deletePersonaHumana(personaHumanaDto);

                    if (respuesta.IsSuccess)
                    {

                        notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Success,
                            Duration = 3000,
                            Summary = "Se ha eliminado correctamente"
                        });
                        listPersonaHumana.Remove(personaHumanaDto);

                        await grid.Reload();
                        StateHasChanged();
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Message = ex.Message;
                    notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Duration = 3000,
                        Summary = "No se ha podido eliminar"
                    });
                }

            }


            return respuesta;
        }

    }
}