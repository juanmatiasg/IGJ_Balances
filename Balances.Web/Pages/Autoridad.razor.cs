using Balances.DTO;
using Balances.Web.Services.FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;


namespace Balances.Web.Pages
{
    public partial class Autoridad
    {


        private string[] tiposDocumentos =
       {
            "DNI",
            "Pasaporte",
            "Cédula"
        };

        private string[] cargos = {
            "Presidente",
            "Vicepresidente",
            "Director Titular",
            "Director Suplente",
            "Administrador Titular",
            "Administrador Suplente",
            "Gerente Titular",
            "Gerente Suplente",
            "Consejo de Vigilancia - Titular",
            "Consejo de Vigilancia - Suplente",
            "Sindicatura - Titular",
            "Sindicatura - Suplente",
            "Comisión Fiscalizadora - Titular",
            "Comisión Fiscalizadora - Suplente"
        };

        ResponseDTO<BalanceDto> rsp = new();
        [Parameter]
        public string? TipoEntidad { get; set; }


        private AutoridadDto modelo = new AutoridadDto();
        private List<AutoridadDto> listAutoridades = new List<AutoridadDto>();




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
            //ResponseDTO<BalanceDto> rsp = new();
            try
            {
                sesionId = await sessionStorage.GetItemAsync<string>("SessionId");
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
                            resultAutoridades(rsp.Result.Autoridades);
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

        private async Task<ResponseDTO<BalanceDto>> insertAutoridad()
        {
            ResponseDTO<BalanceDto> respuesta = new();
            try
            {
                AutoridadValidator autoridadValidator = new();
                ValidationResult result = autoridadValidator.Validate(modelo);

                if (result.IsValid)
                {
                    if (modelo.EsFirmante && listAutoridades.Count(a => a.EsFirmante) >= 1)
                    {
                        ShowDialogFirmantes();
                    }
                    else
                    {
                        modelo.SesionId = sesionId;
                        respuesta = await autoridadService.insertAutoridad(modelo);
                        if (respuesta.IsSuccess)
                        {
                            resultAutoridades(respuesta.Result.Autoridades);
                            // Limpiar los campos después de una inserción exitosa
                            cleanInputs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }

        private void cleanInputs()
        {
            // Restablecer los valores de los campos a su estado inicial o vacío
            modelo = new AutoridadDto();
        }

        private void resultAutoridades(List<AutoridadDto> listAutoridades)
        {
            if (listAutoridades.Count > 0)
            {
                this.listAutoridades = listAutoridades;
            }
        }

        private async Task<ResponseDTO<BalanceDto>> deleteAutoridad(AutoridadDto autoridad)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            autoridad.SesionId = sesionId;
            try
            {
                respuesta = await autoridadService.deleteAutoridad(autoridad);
                if (respuesta.IsSuccess)
                {
                    listAutoridades = respuesta.Result.Autoridades;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }


    }




}