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
using Balances.Web.Services.FluentValidation;
using FluentValidation.Results;

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
            "C�dula"
        };
        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        private ContadorDto modelo = new ContadorDto();
     
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
                            modelo.EsSocioEstudio = rsp.Result.Contador.EsSocioEstudio;
                            modelo.Observaciones = rsp.Result.Contador.Observaciones;
                            modelo.Opinion = rsp.Result.Contador.Opinion;
                            modelo.TomoEstudio = rsp.Result.Contador.TomoEstudio;
                            modelo.FolioEstudio = rsp.Result.Contador.FolioEstudio;
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

        private async Task<ResponseDTO<BalanceDto>> postContador()
        {
            ResponseDTO<BalanceDto> respuesta = new();
            respuesta.IsSuccess = false;
            try
            {

                ContadorValidator contadorValidator = new();

                modelo.SesionId = sesionId;


                ValidationResult result = contadorValidator.Validate(modelo);

                if (result.IsValid)
                {
                
                    respuesta = await contadorService.postContador(modelo);

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