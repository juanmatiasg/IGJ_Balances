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
    public partial class Integrante
    {
        [Parameter]
        public string? TipoEntidad { get; set; }

        RadzenGrid<PersonaHumanaDto> grid;


        private PersonaHumanaDto modelPersonaHumana = new PersonaHumanaDto();
        private List<PersonaHumanaDto> listPersonaHumana = new List<PersonaHumanaDto>();
       

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
                            resultPersonaHumana(rsp.Result.Socios.PersonasHumanas);
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

        private async Task<ResponseDTO<BalanceDto>> addPersonaHumana()
        {
            ResponseDTO<BalanceDto> respuesta = new();
            try
            {
                modelPersonaHumana.SesionId = sesionId;

                PersonaHumanaValidator personaHumanaValidator = new();
                ValidationResult result = personaHumanaValidator.Validate(modelPersonaHumana);

                if (result.IsValid)
                {
                    listPersonaHumana.Add(modelPersonaHumana);
                    respuesta = await socioService.insertPersonaHumana(modelPersonaHumana);

                    if (respuesta.IsSuccess)
                    {                  
                        cleanInputsHumana();
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
                  
                    resultPersonaHumana(respuesta.Result!.Socios.PersonasHumanas);
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