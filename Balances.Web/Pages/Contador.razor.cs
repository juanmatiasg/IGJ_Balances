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
using Balances.Utilities;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using System.Globalization;

namespace Balances.Web.Pages
{
    public partial class Contador
    {
        [Parameter]
        public string? TipoEntidad { get; set; }

        private string[] tiposDocumentos =
        {
            "DNI",
            "Pasaporte",
            "Cédula"
        };
        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        private ContadorDto modelo = new ContadorDto();
        private string msgErrorNombre = "";
        private string msgErrorApellido = "";
        private string msgErrorNroDoc = "";
        private string msgErrorFolio = "";
        private string msgErrorTomo = "";
        private string msgErrorNroLegInf = "";
        private string msgErrorNroIdFiscal = "";
        private string msgErrorFechaInformeAuditorExt = "";
        private string msgErrorTipoDocumento = "";
        protected override async Task OnInitializedAsync()
        {
            await Load();
            await base.OnInitializedAsync();
        }

       

        private async Task<ResponseDTO<BalanceDto>> Load()
        {
            ResponseDTO<BalanceDto> rsp = new();
            try
            {
                sesionId = await sessionStorage.GetItemAsync<string>("SessionId");
                if (sesionId == null)
                {
                    var sesionRespuesta = await sesionService.getNewSession();
                    sesionId = sesionRespuesta.Result;
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
                            modelo.id = rsp.Result.Id;
                            modelo.Nombre = rsp.Result.Contador.Nombre;
                            modelo.Apellido = rsp.Result.Contador.Apellido;
                            modelo.TipoDocumento = rsp.Result.Contador.TipoDocumento;
                            modelo.NroDocumento = rsp.Result.Contador.NroDocumento;
                            modelo.NroFiscal = rsp.Result.Contador.NroFiscal;
                            modelo.Tomo = rsp.Result.Contador.Tomo;
                            modelo.Folio = rsp.Result.Contador.Folio;
                            modelo.FechaInformeAuditorExt = rsp.Result.Contador.FechaInformeAuditorExt;
                            modelo.NroLegalInfoAudExt = rsp.Result.Contador.NroLegalInfoAudExt;
                        }
                        else
                        {
                            rsp.Message = $"error al obtener el contador {rsp.Message}";
                        }
                    }

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                rsp.Message = $"error al obtener el contador {ex.Message}";
            }

            return rsp;
        }

        private async Task<ResponseDTO<BalanceDto>> PostContador()
        {
            ResponseDTO<BalanceDto> respuesta = new();
            respuesta.IsSuccess = false;
            try
            {
                if (checkData())
                {
                    modelo.SesionId = sesionId;
                    //modelo.TipoDocumento = tipoDocSeleccionado;
                    respuesta = await contadorService.postContador(modelo);
                    respuesta.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }

        private bool checkData()
        {
            // Apellido
            if (!string.IsNullOrEmpty(modelo.Nombre))
            {
                if (Validator.IsNumeric(modelo.Nombre))
                {
                    msgErrorNombre = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    msgErrorNombre = "";
                }
            }
            else
            {
                msgErrorNombre = "El campo no puede estar vacio";
                return false;
            }

            // Apellido
            if (!string.IsNullOrEmpty(modelo.Apellido))
            {
                if (Validator.IsNumeric(modelo.Apellido))
                {
                    msgErrorApellido = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    msgErrorApellido = "";
                }
            }
            else
            {
                msgErrorApellido = "El campo no puede estar vacio";
                return false;
            }

            // TipoDocumento
            if (string.IsNullOrEmpty(modelo.TipoDocumento))
            {
                msgErrorTipoDocumento = "Debes seleccionar un tipo de documento";
                return false;
            }
            else
            {
                msgErrorTipoDocumento = "";
            }

            // NroDocumento
            if (!string.IsNullOrEmpty(modelo.NroDocumento))
            {
                if (!Validator.IsNumeric(modelo.NroDocumento))
                {
                    msgErrorNroDoc = "No puedes ingresar caracteres en el campo NroDocumento";
                    return false;
                }
                else
                {
                    msgErrorNroDoc = "";
                }
            }
            else
            {
                msgErrorNroDoc = "El campo no puede estar vacio";
                return false;
            }

            // NroFiscal
            if (!string.IsNullOrEmpty(modelo.NroFiscal))
            {
                if (!Validator.IsNumeric(modelo.NroFiscal))
                {
                    msgErrorNroIdFiscal = "No puedes ingresar caracteres en el campo NroFiscal";
                    return false;
                }
                else
                {
                    msgErrorNroIdFiscal = "";
                }
            }
            else
            {
                msgErrorNroIdFiscal = "El campo no puede estar vacio";
                return false;
            }

            // Tomo
            if (!string.IsNullOrEmpty(modelo.Tomo))
            {
                msgErrorTomo = "";
            }
            else
            {
                msgErrorTomo = "El campo no puede estar vacio";
                return false;
            }

            // Folio
            if (!string.IsNullOrEmpty(modelo.Folio))
            {
                msgErrorFolio = "";
            }
            else
            {
                msgErrorFolio = "El campo no puede estar vacio";
                return false;
            }

            // FechaInformeAuditorExt
            if (modelo.FechaInformeAuditorExt != null)
            {
                msgErrorFechaInformeAuditorExt = "";
            }
            else
            {
                msgErrorFechaInformeAuditorExt = "Seleccioná la fecha correspondiente";
                return false;
            }

            // NroLegalInfoAudExt
            if (!string.IsNullOrEmpty(modelo.NroLegalInfoAudExt))
            {
                if (!Validator.IsNumeric(modelo.NroLegalInfoAudExt))
                {
                    msgErrorNroLegInf = "No puedes ingresar caracteres en el campo NroLegalInfoAudExt";
                    return false;
                }
                else
                {
                    msgErrorNroLegInf = "";
                }
            }
            else
            {
                msgErrorNroLegInf = "El campo no puede estar vacio";
                return false;
            }

            msgErrorNombre = "";
            msgErrorTomo = "";
            msgErrorApellido = "";
            msgErrorNroIdFiscal = "";
            msgErrorNroLegInf = "";
            msgErrorNroDoc = "";
            msgErrorTipoDocumento = "";
            msgErrorFechaInformeAuditorExt = "";
            return true;
        }
    }
}