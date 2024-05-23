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
using Balances.Web.Services.FluentValidation;
using FluentValidation.Results;

namespace Balances.Web.Pages
{
    public partial class IntegranteJuridico
    {
        RadzenGrid<PersonaJuridicaDto> grid;


        [Parameter]
        public string? TipoEntidad { get; set; }

        private PersonaJuridicaDto model = new PersonaJuridicaDto();
        private List<PersonaJuridicaDto> listPersonaJuridica = new List<PersonaJuridicaDto>();
        
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
                            resultPersonaJuridica(rsp.Result.Socios.PersonasJuridicas);
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
     

        private async Task<ResponseDTO<BalanceDto>> InsertPersonaJuridica()
        {
            
                ResponseDTO<BalanceDto> respuesta = new();
                try
                {
                   
                    model.SesionId = sesionId;
                    PersonaJuridicaValidator personaJuridicaValidator = new();
                    ValidationResult result = personaJuridicaValidator.Validate(model);

                    if (result.IsValid)
                    {
                       
                        respuesta = await socioService.insertPersonaJuridica(model);

                        if (respuesta.IsSuccess)
                        {
                            resultPersonaJuridica(respuesta.Result!.Socios.PersonasJuridicas);
                            cleanInputsJuridica();
                            await grid.Reload();
                            StateHasChanged();
                        }
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Message = ex.Message;
                }

                return respuesta;
           
        }

        private void cleanInputsJuridica()
        {
            // Restablecer los valores de los campos a su estado inicial o vacío
            model = new PersonaJuridicaDto();
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
                personaJuridicaDto.SesionId = sesionId;
                respuesta = await socioService.deletePersonaJuridica(personaJuridicaDto);
                
                if (respuesta.IsSuccess)
                {
                 
                    listPersonaJuridica.Remove(personaJuridicaDto);
                    await grid.Reload();
                    StateHasChanged();
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