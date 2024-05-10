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
using System.Text;
using System.Reflection;

namespace Balances.Web.Pages
{
    public partial class Libros
    {
        private LibrosDto modelo = new LibrosDto();
        
        
        [Parameter]
        public string? TipoEntidad { get; set; }

        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        protected override async void OnInitialized()
        {
            await Load();
            base.OnInitialized();
        }


    

        private async Task HandleChange(LibroDto libro)
        {
            // Encuentra el libro correspondiente en el modelo y actualiza sus valores
            if (libro != null)
            {
                if (modelo.Asamblea == libro)
                {
                    modelo.Asamblea = libro;
                }
                else if (modelo.Administracion == libro)
                {
                    modelo.Administracion = libro;
                }
                else if (modelo.AsistenciaAsamblea == libro)
                {
                    modelo.AsistenciaAsamblea = libro;
                }
                else if (modelo.Auditor == libro)
                {
                    modelo.Auditor = libro;
                }
                else if (modelo.Efectivo == libro)
                {
                    modelo.Efectivo = libro;
                }
                else if (modelo.Fiscalizacion == libro)
                {
                    modelo.Fiscalizacion = libro;
                }
                else if (modelo.IVA == libro)
                {
                    modelo.IVA = libro;
                }
                else if (modelo.IVACompras == libro)
                {
                    modelo.IVACompras = libro;
                }
                else if (modelo.IVAVentas == libro)
                {
                    modelo.IVAVentas = libro;
                }
                else if (modelo.Resultados == libro)
                {
                    modelo.Resultados = libro;
                }
                else if (modelo.EstadosContablesConsolidados == libro)
                {
                    modelo.EstadosContablesConsolidados = libro;
                }
                else if (modelo.PatrimonioNeto == libro)
                {
                    modelo.PatrimonioNeto = libro;
                }
                else if (modelo.SituacionPatrimonial == libro)
                {
                    modelo.SituacionPatrimonial = libro;
                }
                else if (modelo.Memoria == libro)
                {
                    modelo.Memoria = libro;
                }
                else if (modelo.Informacion == libro)
                {
                    modelo.Informacion = libro;
                }
            }

            await insertLibros(modelo);
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
                            setLibros(rsp.Result.Libros);
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

        private void setLibros(LibrosDto libros)
        {
            if (libros != null)
            {
                this.modelo = libros;
            }
        }

        private async Task<ResponseDTO<BalanceDto>> insertLibros(LibrosDto libros)
        {
            ResponseDTO<BalanceDto> respuesta = new();
            try
            {
                libros.SessionId = sesionId;
                respuesta = await serviceLibro.insertLibros(libros);
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }
    }
}