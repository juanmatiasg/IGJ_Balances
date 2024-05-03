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
    public partial class Integrante
    {
        [Parameter]
        public string? TipoEntidad { get; set; }

        private string idSession = "";
        private PersonaHumanaDto modelPersonaHumana = new PersonaHumanaDto();
        private List<PersonaHumanaDto> listPersonaHumana = new List<PersonaHumanaDto>();
        private string msgErrorNombre = "";
        private string msgErrorApellido = "";
        private string msgErrorTipoDoc = "";
        private string msgErrorNroDoc = "";
        private string msgErrorCuit = "";
        private string msgErrorCuotas = "";
        private string msgErrorVotos = "";
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
                            resultPersonaHumana(rsp.Result.Socios.PersonasHumanas);
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

        private async Task<ResponseDTO<BalanceDto>> addPersonaHumana()
        {
            if (checkData())
            {
                ResponseDTO<BalanceDto> respuesta = new();
                try
                {
                    modelPersonaHumana.SesionId = sesionId;
                    respuesta = await socioService.insertPersonaHumana(modelPersonaHumana);
                    if (respuesta.IsSuccess)
                    {
                        resultPersonaHumana(respuesta.Result.Socios.PersonasHumanas);
                        cleanInputsHumana();
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

        private void cleanInputsHumana()
        {
            // Restablecer los valores de los campos a su estado inicial o vacío
            modelPersonaHumana = new PersonaHumanaDto();
        }

        private void resultPersonaHumana(List<PersonaHumanaDto> listPersonaHumana)
        {
            this.listPersonaHumana = listPersonaHumana;
        }

        private async Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumanaDto)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            try
            {
                personaHumanaDto.SesionId = sesionId;
                respuesta = await socioService.deletePersonaHumana(personaHumanaDto);
                if (respuesta.IsSuccess)
                {
                    listPersonaHumana = respuesta.Result.Socios.PersonasHumanas;
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
            if (!string.IsNullOrEmpty(modelPersonaHumana.Nombre))
            {
                if (Validator.IsNumeric(modelPersonaHumana.Nombre))
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
            if (!string.IsNullOrEmpty(modelPersonaHumana.Apellido))
            {
                if (Validator.IsNumeric(modelPersonaHumana.Apellido))
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

            //Tipo Doc
            if (!string.IsNullOrEmpty(modelPersonaHumana.TipoDocumento))
            {
                msgErrorTipoDoc = "";
            }
            else
            {
                msgErrorTipoDoc = "El campo no puede estar vacio";
                return false;
            }

            // NroDocumento
            if (!string.IsNullOrEmpty(modelPersonaHumana.NroDocumento))
            {
                if (!Validator.IsNumeric(modelPersonaHumana.NroDocumento))
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

            // CUIT
            if (!string.IsNullOrEmpty(modelPersonaHumana.NroFiscal))
            {
                if (!Validator.IsNumeric(modelPersonaHumana.NroFiscal))
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
            if (!string.IsNullOrEmpty(modelPersonaHumana.Cuotas))
            {
                if (!Validator.IsNumeric(modelPersonaHumana.Cuotas))
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
            if (!string.IsNullOrEmpty(modelPersonaHumana.Votos))
            {
                if (!Validator.IsNumeric(modelPersonaHumana.Votos))
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
            msgErrorNombre = "";
            msgErrorApellido = "";
            msgErrorTipoDoc = "";
            msgErrorNroDoc = "";
            msgErrorCuit = "";
            msgErrorCuotas = "";
            msgErrorVotos = "";
            return true;
        }
    }
}