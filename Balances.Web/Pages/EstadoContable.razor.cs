using Balances.DTO;
using Balances.Model;
using Balances.Web.Services;
using Balances.Web.Services.FluentValidation;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using global::Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.Globalization;

namespace Balances.Web.Pages
{
    public partial class EstadoContable
    {
        private CultureInfo argentinianCulture = new CultureInfo("es-AR");

        RadzenGrid<RubroPatrimonioNetoDto> grid;

        [Parameter]
        public string? TipoEntidad { get; set; }

        private string denominacionDto = "";
        private decimal importeDto = 0;

        private RubroPatrimonioNetoDto rubroDto = new RubroPatrimonioNetoDto();
        private EstadoContableDto estadoContableDto = new EstadoContableDto();

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        private string[] tipoDeBalance =
      {
            "Anual",
            "Irregular",
            "De liquidación",
            "Otro"
        };

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

                            estadoContableDto.fechaInicio = rsp.Result.Caratula.FechaInicio;
                            estadoContableDto.fechaEstado = rsp.Result.Caratula.FechaDeCierre;
                            estadoContableDto = new EstadoContableDto(rsp.Result.EstadoContable);
                        }
                    }
                }
                await grid.Reload();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}");
            }
        }

        private async Task<ResponseDTO<BalanceDto>> insertEECC()
        {

            estadoContableDto.totalActivo = SumaEECC.TotalActivo(estadoContableDto);
            estadoContableDto.totalPasivo = SumaEECC.TotalPasivo(estadoContableDto);
            estadoContableDto.activoCorriente = SumaEECC.ActivoCorriente(estadoContableDto);
            estadoContableDto.activoNoCorriente = SumaEECC.ActivoNoCorriente(estadoContableDto);
            estadoContableDto.patrimonioNeto = SumaEECC.PatrimonioNeto(estadoContableDto);
            decimal OtrosRubrosPatrimonioNeto = SumaEECC.OtrosRubrosPatrimonioNeto(estadoContableDto);

            ResponseDTO<BalanceDto> rsp = new();
            try
            {
                if (estadoContableDto.patrimonioNeto != OtrosRubrosPatrimonioNeto)
                {
                    var alerta = await swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Error en el balanceo",
                        Text = $"El total del patrimonio neto: ${estadoContableDto.patrimonioNeto} ARS no coincide con el total de Rubros del Patrimonio neto: ${OtrosRubrosPatrimonioNeto} ARS",
                        Icon = SweetAlertIcon.Warning,
                        ConfirmButtonText = "Cerrar"

                    });

                }
                StateHasChanged();


                estadoContableDto.SesionId = sesionId;
                rsp = await estadoContableService.insertEECC(estadoContableDto);
                if (rsp.IsSuccess)
                {
                    rsp.Message = "Se inserto el Estado Contable sastifactoriamente";

                }
                else
                {
                    rsp.Message = "No se inserto el Estado Contable";
                }


            }
            catch (Exception ex)
            {
                rsp.Message = ex.Message;
            }

            return rsp;
        }


        private async Task<ResponseDTO<BalanceDto>> insertRubro()
        {
            var rsp = new ResponseDTO<BalanceDto>();
            try
            {
                rubroDto.codigo = Guid.NewGuid().ToString();
                rubroDto.denominacion = denominacionDto;
                rubroDto.importe = importeDto;
                rubroDto.SesionId = sesionId;
                RubroDtoValidator rubrodtovalidator = new();
                ValidationResult resultadoValidacion = rubrodtovalidator.Validate(rubroDto);


                if (resultadoValidacion.IsValid)
                {

                    rsp = await estadoContableService.insertRubro(rubroDto);
                    if (rsp.IsSuccess)
                    {
                        var result = rsp.Result;
                        if (result != null)
                        {
                            setListOtrosRubros(result.EstadoContable.OtrosRubros);
                            rsp.Message = "Se inserto el rubro sastifactoriamente";
                            await grid.Reload();
                            StateHasChanged();
                        }
                    }
                    else
                    {
                        rsp.Message = "No se inserto el rubro";
                    }
                }
            }
            catch (Exception ex)
            {
                rsp.Message = ex.Message;
            }

            return rsp;
        }

        private async Task<ResponseDTO<BalanceDto>> deleteRubro(RubroPatrimonioNetoDto rubroDto)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            try
            {
                rubroDto.SesionId = sesionId;
                respuesta = await estadoContableService.deleteRubro(rubroDto);

                if (respuesta.IsSuccess)
                {
                    estadoContableDto.otrosRubros.Remove(rubroDto);
                    respuesta.Message = "Se eliminó el rubro sastifactoriamente";

                    await grid.Reload();
                    StateHasChanged();
                }
                else
                {
                    respuesta.Message = "No se eliminó el rubro sastifactoriamente";
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }


        public void setListOtrosRubros(List<RubroPatrimonioNeto> rubros)
        {
            if (rubros != null)
            {
                estadoContableDto.otrosRubros.Clear();
                foreach (var rubro in rubros)
                {
                    estadoContableDto.otrosRubros.Add(new RubroPatrimonioNetoDto { codigo = rubro.Codigo, denominacion = rubro.Denominacion, importe = rubro.Importe, });
                }
            }
        }




    }
}