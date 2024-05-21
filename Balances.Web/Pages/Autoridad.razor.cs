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
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FluentValidation.Results;
using Balances.Web.Services.FluentValidation;

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
            ResponseDTO<BalanceDto> respuesta = new();
            try
            {
                AutoridadValidator autoridadValidator = new();
                ValidationResult result = autoridadValidator.Validate(modelo);

                if (result.IsValid)
                {
                    if (modelo.EsFirmante && listAutoridades.Count(a => a.EsFirmante) >= 1)
                    {
                        await ShowDialogFirmantes();
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