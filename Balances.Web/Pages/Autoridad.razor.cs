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

        [Parameter]
        public string? TipoEntidad { get; set; }

        [CascadingParameter]
        IModalService Modal { get; set; } = default !;

        private string idSession = "";
        private AutoridadDto modelo = new AutoridadDto();
        private List<AutoridadDto> listAutoridades = new List<AutoridadDto>();
        private string resultNameAndSurname = "";
        private string msgErrorNombre = "";
        private string msgErrorApellido = "";
        private string msgErrorNroIdFiscal = "";
        private string msgErrorNroDoc = "";
        private string msgErrorCargo = "";
        private string msgErrorTipoDeDoc = "";
        private string nroIdentificacionSocial = "Nro de Identificación Fiscal";
        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        // reference to the modal component
        private Modal? modalRef;
        private Task ShowModal()
        {
            return modalRef.Show();
        }

        private Task HideModal()
        {
            return modalRef.Hide();
        }

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
            if (checkData())
            {
                ResponseDTO<BalanceDto> respuesta = new();
                try
                {
                    // Check if the current autoridad is a firmante
                    if (modelo.EsFirmante && listAutoridades.Count(a => a.EsFirmante) >= 1)
                    {
                        ShowModal();
                        return null;
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
                catch (Exception ex)
                {
                    respuesta.Message = ex.Message;
                }

                return respuesta;
            }
            else
            {
                return null;
            }
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

        private bool checkData()
        {
            // Nombre
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

            // Apellido
            if (!string.IsNullOrEmpty(modelo.TipoDocumento))
            {
                msgErrorTipoDeDoc = "";
            }
            else
            {
                msgErrorTipoDeDoc = "El campo no puede estar vacio";
                return false;
            }

            // NroDocumento
            if (!string.IsNullOrEmpty(modelo.NroDocumento))
            {
                if (!Validator.IsNumeric(modelo.NroDocumento))
                {
                    msgErrorNroDoc = "No puedes ingresar caracteres";
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
                    msgErrorNroIdFiscal = "No puedes ingresar caracteres";
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

            // Cargo
            if (!string.IsNullOrEmpty(modelo.Cargo))
            {
                if (Validator.IsNumeric(modelo.Cargo))
                {
                    msgErrorCargo = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    msgErrorCargo = "";
                }
            }
            else
            {
                msgErrorCargo = "El campo no puede estar vacio";
                return false;
            }

            // Si todos los campos pasan la validación, devuelve true
            msgErrorTipoDeDoc = "";
            msgErrorNombre = "";
            msgErrorApellido = "";
            msgErrorNroIdFiscal = "";
            msgErrorNroDoc = "";
            msgErrorCargo = "";
            return true;
        }
    }
}