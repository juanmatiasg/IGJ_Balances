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
    public partial class IntegranteJuridico
    {
        [Parameter]
        public string? TipoEntidad { get; set; }

        private string idSession = "";
        private PersonaJuridicaDto modelPersonaJuridica = new PersonaJuridicaDto();
        private List<PersonaJuridicaDto> listPersonaJuridica = new List<PersonaJuridicaDto>();
        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        private string msgErrorDenominacion = "";
        private string msgErrorCuit = "";
        private string msgErrorCuotas = "";
        private string msgErrorVotos = "";
        protected override async Task OnInitializedAsync()
        {
            await Load();
            await base.OnInitializedAsync();
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
                            resultPersonaJuridica(rsp.Result.Socios.PersonasJuridicas);
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

        private async Task<ResponseDTO<BalanceDto>> addPersonaJuridica()
        {
            if (checkData())
            {
                ResponseDTO<BalanceDto> respuesta = new();
                try
                {
                    modelPersonaJuridica.SesionId = sesionId;
                    respuesta = await socioService.insertPersonaJuridica(modelPersonaJuridica);
                    if (respuesta.IsSuccess)
                    {
                        resultPersonaJuridica(respuesta.Result.Socios.PersonasJuridicas);
                        // Limpiar los campos después de una inserción exitosa
                        cleanInputsJuridica();
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

        private void cleanInputsJuridica()
        {
            // Restablecer los valores de los campos a su estado inicial o vacío
            modelPersonaJuridica = new PersonaJuridicaDto();
        }

        private void resultPersonaJuridica(List<PersonaJuridicaDto> listPersonaJuridica)
        {
            this.listPersonaJuridica = listPersonaJuridica;
        }

        private async Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            try
            {
                respuesta = await socioService.deletePersonaJuridica(personaJuridicaDto);
                if (respuesta.IsSuccess)
                {
                    listPersonaJuridica = respuesta.Result.Socios.PersonasJuridicas;
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
            if (!string.IsNullOrEmpty(modelPersonaJuridica.Denominacion))
            {
                if (Validator.IsNumeric(modelPersonaJuridica.Denominacion))
                {
                    msgErrorDenominacion = "No puedes ingresar un valor numérico";
                    return false;
                }
                else
                {
                    msgErrorDenominacion = "";
                }
            }
            else
            {
                msgErrorDenominacion = "El campo no puede estar vacio";
                return false;
            }

            // Nro id Fiscal
            if (!string.IsNullOrEmpty(modelPersonaJuridica.NroFiscal))
            {
                if (!Validator.IsNumeric(modelPersonaJuridica.NroFiscal))
                {
                    msgErrorCuit = "No puedes ingresar caracteres";
                    return false;
                }
                else
                {
                    msgErrorCuit = "";
                }
            }
            else
            {
                msgErrorCuit = "El campo no puede estar vacio";
                return false;
            }

            // Cuotas
            if (!string.IsNullOrEmpty(modelPersonaJuridica.Cuotas))
            {
                if (!Validator.IsNumeric(modelPersonaJuridica.Cuotas))
                {
                    msgErrorCuotas = "No puedes ingresar caracteres";
                    return false;
                }
                else
                {
                    msgErrorCuotas = "";
                }
            }
            else
            {
                msgErrorCuotas = "El campo no puede estar vacio";
                return false;
            }

            // Votos
            if (!string.IsNullOrEmpty(modelPersonaJuridica.Votos))
            {
                if (!Validator.IsNumeric(modelPersonaJuridica.Votos))
                {
                    msgErrorVotos = "No puedes ingresar caracteres";
                    return false;
                }
                else
                {
                    msgErrorVotos = "";
                }
            }
            else
            {
                msgErrorVotos = "El campo no puede estar vacio";
                return false;
            }

            // Si todos los campos pasan la validación, devuelve true
            msgErrorDenominacion = "";
            msgErrorCuit = "";
            msgErrorCuotas = "";
            msgErrorVotos = "";
            return true;
        }
    }
}