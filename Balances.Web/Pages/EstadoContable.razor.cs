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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Http;

namespace Balances.Web.Pages
{
    public partial class EstadoContable
    {
        private CultureInfo argentinianCulture = new CultureInfo("es-AR");
        [Parameter]
        public string? TipoEntidad { get; set; }

        private string tipoBalanceError = "";
        private string fechaInicioError = "";
        private string fechaAsambleaError = "";
        private string fechaEstadoError = "";
        private string fechaReunionDirectorioError = "";
        private string cajaYBancosError = "";
        private string inversionesError = "";
        private string bienesDeCambioError = "";
        private string activoCorrienteRestanteError = "";
        private string activoNoCorrienteRestanteError = "";
        private string ajusteCapitalError = "";
        private string aportesIrrevocablesError = "";
        private string bienesDeUsoError = "";
        private string capitalSuscriptoError = "";
        private string deudasPasivoCorrienteError = "";
        private string deudasPasivoNoCorrienteError = "";
        private string gananciasPerdidasEjercicioError = "";
        private string gananciasReservadasError = "";
        private string perdidasAcumuladasError = "";
        private string primaEmisionError = "";
        private string propiedadesDeInversionError = "";
        private string reservaLegalError = "";
        //OtrosRubros
        private string denominacionError = "";
        private string pasivoNoCorrienteError = "";
        private string denominacionDto = "";
        private decimal importeDto = 0;
        private RubroPatrimonioNetoDto rubroDto = new RubroPatrimonioNetoDto();
        private EstadoContableDto estadoContableDto = new EstadoContableDto();
        private Boolean statusPatrimonioNeto = false;
        private string otraCosa = "";
        decimal cantidad;
        private bool firstTimeInput = true;
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
                            estadoContableDto = new EstadoContableDto(rsp.Result.EstadoContable);
                        }
                    }
                }

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}");
            }
        }

        private async Task<ResponseDTO<BalanceDto>> insertEECC()
        {
            sumatoriaTotalActivoCorriente();
            sumatoriaTotalActivoNoCorriente();
            sumatorialTotalActivo();
            sumatoriaTotalPasivo();
            resultadoPatrimonioNeto();
            //resultadoPatNetDetailPatNeto();
            ResponseDTO<BalanceDto> rsp = new();
            try
            {
                if (checkData())
                {
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
                if (checkDataOtrosRubros())
                {
                    rubroDto.codigo = Guid.NewGuid().ToString();
                    rubroDto.denominacion = denominacionDto;
                    rubroDto.importe = importeDto;
                    rubroDto.SesionId = sesionId;
                    rsp = await estadoContableService.insertRubro(rubroDto);
                    if (rsp.IsSuccess)
                    {
                        var result = rsp.Result;
                        if (result != null)
                        {
                            setListOtrosRubros(result.EstadoContable.OtrosRubros);
                            rsp.Message = "Se inserto el rubro sastifactoriamente";
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

        private void sumatoriaTotalActivoCorriente()
        {
            var sumatoriaTotalActivoCorriente = Convert.ToDecimal(estadoContableDto.cajaYBancos) + estadoContableDto.inversionesActivoCorriente + estadoContableDto.bienesDeCambio + estadoContableDto.activoCorrienteRestante;
            estadoContableDto.activoCorriente = sumatoriaTotalActivoCorriente;
        }

        private void sumatoriaTotalActivoNoCorriente()
        {
            var sumatoriaTotalActivoNoCorriente = estadoContableDto.bienesDeCambio + estadoContableDto.propiedadesDeInversion + estadoContableDto.inversionesActivoNoCorriente + estadoContableDto.activoNoCorrienteRestante;
            estadoContableDto.activoNoCorriente = sumatoriaTotalActivoNoCorriente;
        }

        private void sumatorialTotalActivo()
        {
            var sumatorialTotalActivo = estadoContableDto.activoCorriente + estadoContableDto.activoNoCorriente;
            estadoContableDto.totalActivo = sumatorialTotalActivo;
        }

        private void sumatoriaTotalPasivo()
        {
            var sumatoriaTotalPasivo = estadoContableDto.deudorPasivoCorriente + estadoContableDto.deudorPasivoNoCorriente;
            estadoContableDto.totalPasivo = sumatoriaTotalPasivo;
        }

        private decimal resultadoPatrimonioNeto()
        {
            if (estadoContableDto.totalActivo != 0 && estadoContableDto.totalPasivo != 0)
            {
                return estadoContableDto.patrimonioNeto = estadoContableDto.totalActivo - estadoContableDto.totalPasivo;
            }
            else
            {
                return 0;
            }
        }

        private bool resultadoPatNetDetailPatNeto()
        {
            if (estadoContableDto.otrosRubros.Count > 0)
            {
                decimal sumatoriaImporte = 0;
                foreach (var rubros in estadoContableDto.otrosRubros)
                {
                    sumatoriaImporte = sumatoriaImporte + rubros.importe;
                }

                var sumatoriaDetallePatNeto = estadoContableDto.capitalSuscripto + estadoContableDto.ajusteCapital + estadoContableDto.aportesIrrevocables + estadoContableDto.primaEmision + estadoContableDto.gananciasReservadas + estadoContableDto.perdidasAcumuladas + estadoContableDto.gananciasPerdidasEjercicio + estadoContableDto.reservaLegal + sumatoriaImporte;
                if (resultadoPatrimonioNeto().Equals(sumatoriaDetallePatNeto))
                {
                    return statusPatrimonioNeto = false;
                }
                else
                {
                    return statusPatrimonioNeto = true;
                }
            }
            else
            {
                var sumatoriaDetallePatNeto = estadoContableDto.capitalSuscripto + estadoContableDto.ajusteCapital + estadoContableDto.aportesIrrevocables + estadoContableDto.primaEmision + estadoContableDto.gananciasReservadas + estadoContableDto.perdidasAcumuladas + estadoContableDto.gananciasPerdidasEjercicio + estadoContableDto.reservaLegal;
                if (resultadoPatrimonioNeto().Equals(sumatoriaDetallePatNeto))
                {
                    return statusPatrimonioNeto = false;
                }
                else
                {
                    return statusPatrimonioNeto = true;
                }
            }

            return statusPatrimonioNeto;
        }

        private bool checkDataOtrosRubros()
        {
            // Denominacion
            if (!string.IsNullOrEmpty(denominacionDto))
            {
                if (Validator.IsNumeric(denominacionDto))
                {
                    denominacionError = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    denominacionError = "";
                }
            }
            else
            {
                denominacionError = "El campo no puede estar vacio";
                return false;
            }

            if (importeDto > 0)
            {
                if (!Validator.IsNumeric(importeDto.ToString()))
                {
                    pasivoNoCorrienteError = "No puedes ingresar con caracteres";
                    return false;
                }
                else
                {
                    pasivoNoCorrienteError = "";
                }
            }
            else
            {
                pasivoNoCorrienteError = "El campo no puede estar vacio";
                return false;
            }

            //OtrosRubros
            denominacionError = "";
            pasivoNoCorrienteError = "";
            return true;
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

        private string FormatWithThousandSeparators(decimal value)
        {
            return string.Format("{0:N}", value);
        }

        private bool checkData()
        {
            // Tipo Balance
            if (!string.IsNullOrEmpty(estadoContableDto.tipoBalance))
            {
                if (Validator.IsNumeric(estadoContableDto.tipoBalance))
                {
                    tipoBalanceError = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    tipoBalanceError = "";
                }
            }
            else
            {
                tipoBalanceError = "El campo no puede estar vacio";
                return false;
            }

            // Fecha Inicio
            if (estadoContableDto.fechaInicio == null)
            {
                fechaInicioError = "Seleccioná la fecha correspondiente";
                return false;
            }
            else
            {
                fechaInicioError = "";
            }

            //Fecha Estado
            if (estadoContableDto.fechaEstado == null)
            {
                fechaEstadoError = "Seleccioná la fecha correspondiente";
                return false;
            }
            else
            {
                fechaEstadoError = "";
            }

            //Fecha Reunion Directorio
            if (estadoContableDto.fechaReunionDirectorio == null)
            {
                fechaReunionDirectorioError = "Seleccioná la fecha correspondiente";
                return false;
            }
            else
            {
                fechaReunionDirectorioError = "";
            }

            //Fecha Asamblea
            if (estadoContableDto.fechaAsamblea == null)
            {
                fechaAsambleaError = "Seleccioná la fecha correspondiente";
                return false;
            }
            else
            {
                fechaAsambleaError = "";
            }

            //Caja y Bancos
            if (!string.IsNullOrEmpty(estadoContableDto.cajaYBancos.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.cajaYBancos.ToString()))
                {
                    cajaYBancosError = "No puedes ingresar caracteres en el campo ";
                    return false;
                }
                else
                {
                    cajaYBancosError = "";
                }
            }
            else
            {
                cajaYBancosError = "El campo no puede estar vacio";
                return false;
            }

            //Inversiones
            if (!string.IsNullOrEmpty(estadoContableDto.inversionesActivoCorriente.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.inversionesActivoCorriente.ToString()))
                {
                    inversionesError = "No puedes ingresar caracteres en el campo ";
                    return false;
                }
                else
                {
                    inversionesError = "";
                }
            }
            else
            {
                inversionesError = "El campo no puede estar vacio";
                return false;
            }

            //Bienes de Cambio
            if (!string.IsNullOrEmpty(estadoContableDto.bienesDeCambio.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.bienesDeCambio.ToString()))
                {
                    bienesDeCambioError = "No puedes ingresar caracteres en el campo ";
                    return false;
                }
                else
                {
                    bienesDeCambioError = "";
                }
            }
            else
            {
                bienesDeCambioError = "El campo no puede estar vacio";
                return false;
            }

            //Activo  Corriente Restante
            if (!string.IsNullOrEmpty(estadoContableDto.activoCorrienteRestante.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.activoCorrienteRestante.ToString()))
                {
                    activoCorrienteRestanteError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    activoCorrienteRestanteError = "";
                }
            }
            else
            {
                activoCorrienteRestanteError = "El campo no puede estar vacio";
                return false;
            }

            //Bienes de Uso
            if (!string.IsNullOrEmpty(estadoContableDto.bienesDeUso.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.bienesDeUso.ToString()))
                {
                    bienesDeUsoError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    bienesDeUsoError = "";
                }
            }
            else
            {
                bienesDeUsoError = "El campo no puede estar vacio";
                return false;
            }

            //Propiedades de Inversion
            if (!string.IsNullOrEmpty(estadoContableDto.propiedadesDeInversion.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.propiedadesDeInversion.ToString()))
                {
                    propiedadesDeInversionError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    propiedadesDeInversionError = "";
                }
            }
            else
            {
                propiedadesDeInversionError = "El campo no puede estar vacio";
                return false;
            }

            //Activo No Corriente Restante
            if (!string.IsNullOrEmpty(estadoContableDto.activoNoCorrienteRestante.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.activoNoCorrienteRestante.ToString()))
                {
                    activoNoCorrienteRestanteError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    activoNoCorrienteRestanteError = "";
                }
            }
            else
            {
                activoNoCorrienteRestanteError = "El campo no puede estar vacio";
                return false;
            }

            //Deudas del Pasivo Corriente
            if (!string.IsNullOrEmpty(estadoContableDto.deudorPasivoCorriente.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.capitalSuscripto.ToString()))
                {
                    deudasPasivoCorrienteError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    deudasPasivoCorrienteError = "";
                }
            }
            else
            {
                deudasPasivoCorrienteError = "El campo no puede estar vacio";
                return false;
            }

            //Deudas del Pasivo No Corriente
            if (!string.IsNullOrEmpty(estadoContableDto.deudorPasivoNoCorriente.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.deudorPasivoNoCorriente.ToString()))
                {
                    deudasPasivoNoCorrienteError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    deudasPasivoNoCorrienteError = "";
                }
            }
            else
            {
                deudasPasivoNoCorrienteError = "El campo no puede estar vacio";
                return false;
            }

            //Capital Sucripto
            if (!string.IsNullOrEmpty(estadoContableDto.capitalSuscripto.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.capitalSuscripto.ToString()))
                {
                    capitalSuscriptoError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    capitalSuscriptoError = "";
                }
            }
            else
            {
                capitalSuscriptoError = "El campo no puede estar vacio";
                return false;
            }

            //Ajuste al capital
            if (!string.IsNullOrEmpty(estadoContableDto.ajusteCapital.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.ajusteCapital.ToString()))
                {
                    ajusteCapitalError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    ajusteCapitalError = "";
                }
            }
            else
            {
                ajusteCapitalError = "El campo no puede estar vacio";
                return false;
            }

            //Aporte Irrevocables
            if (!string.IsNullOrEmpty(estadoContableDto.aportesIrrevocables.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.aportesIrrevocables.ToString()))
                {
                    aportesIrrevocablesError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    aportesIrrevocablesError = "";
                }
            }
            else
            {
                aportesIrrevocablesError = "El campo no puede estar vacio";
                return false;
            }

            //Prima Emision
            if (!string.IsNullOrEmpty(estadoContableDto.primaEmision.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.primaEmision.ToString()))
                {
                    primaEmisionError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    primaEmisionError = "";
                }
            }
            else
            {
                primaEmisionError = "El campo no puede estar vacio";
                return false;
            }

            //Ganancias Reservadas
            if (!string.IsNullOrEmpty(estadoContableDto.gananciasReservadas.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.gananciasReservadas.ToString()))
                {
                    gananciasReservadasError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    gananciasReservadasError = "";
                }
            }
            else
            {
                gananciasReservadasError = "El campo no puede estar vacio";
                return false;
            }

            //Perdidas Acumuladas
            if (!string.IsNullOrEmpty(estadoContableDto.perdidasAcumuladas.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.perdidasAcumuladas.ToString()))
                {
                    perdidasAcumuladasError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    perdidasAcumuladasError = "";
                }
            }
            else
            {
                perdidasAcumuladasError = "El campo no puede estar vacio";
                return false;
            }

            //Ganancias  Perdidas del Ejercicio
            if (!string.IsNullOrEmpty(estadoContableDto.gananciasPerdidasEjercicio.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.gananciasPerdidasEjercicio.ToString()))
                {
                    gananciasPerdidasEjercicioError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    gananciasPerdidasEjercicioError = "";
                }
            }
            else
            {
                gananciasPerdidasEjercicioError = "El campo no puede estar vacio";
                return false;
            }

            //Reserva Legal
            if (!string.IsNullOrEmpty(estadoContableDto.reservaLegal.ToString()))
            {
                if (!Validator.IsNumeric(estadoContableDto.reservaLegal.ToString()))
                {
                    reservaLegalError = "No puedes ingresar caracteres en el campo";
                    return false;
                }
                else
                {
                    reservaLegalError = "";
                }
            }
            else
            {
                reservaLegalError = "El campo no puede estar vacio";
                return false;
            }

            tipoBalanceError = "";
            fechaInicioError = "";
            fechaAsambleaError = "";
            fechaEstadoError = "";
            fechaReunionDirectorioError = "";
            cajaYBancosError = "";
            inversionesError = "";
            bienesDeCambioError = "";
            activoCorrienteRestanteError = "";
            activoNoCorrienteRestanteError = "";
            ajusteCapitalError = "";
            aportesIrrevocablesError = "";
            bienesDeUsoError = "";
            capitalSuscriptoError = "";
            deudasPasivoCorrienteError = "";
            deudasPasivoNoCorrienteError = "";
            gananciasPerdidasEjercicioError = "";
            gananciasReservadasError = "";
            primaEmisionError = "";
            perdidasAcumuladasError = "";
            propiedadesDeInversionError = "";
            reservaLegalError = "";
            return true;
        }
    }
}