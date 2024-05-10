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

namespace Balances.Web.Pages
{
    public partial class Libro
    {
        private string msgErrorFolio = "";
        private string msgErrorNroRubrica = "";
        private string msgErrorNombre = "";
        private string msgErrorFechaRubrica = "";
        private string msgErrorFechaUltimaRegistracion = "";
        private string msgErrormsgErrorFolioUltimaRegistracion = "";
        private const string ESTADO_CONTABLE = "Estados Contables Consolidados";
        private const string INFORME_ORGANO = "Informe Organo Fiscalización";
        
        
        [Parameter]
        public LibroDto libroP
        {
            set
            {
                libext.Original = value;
            }

            get
            {
                return libext.Original;
            }
        }

        private LibroDtoExtended libext = new LibroDtoExtended();


        [Parameter]
        public bool NoSabeONoConstesta { get; set; }

        [Parameter]
        public EventCallback<LibroDto> OnBlur { get; set; }

        private async Task OnBlurHandler()
        {
            await OnBlur.InvokeAsync(libroP);
            
            if (checkData())
            {
                 await OnBlur.InvokeAsync(libroP);
            }
        }

        private bool checkData()
        {
            // Nombre
            if (string.IsNullOrEmpty(libroP.Nombre))
            {
                msgErrorNombre = "El campo no puede estar vacío";
                return false;
            }
            else if (Validator.IsNumeric(libroP.Nombre))
            {
                msgErrorNombre = "No puedes ingresar un valor numérico";
                return false;
            }
            else
            {
                msgErrorNombre = "";
            }

            // Nro Rubrica
            if (string.IsNullOrEmpty(libroP.NumeroRubrica))
            {
                msgErrorNroRubrica = "El campo no puede estar vacío";
                return false;
            }
            else if (!Validator.IsNumeric(libroP.NumeroRubrica))
            {
                msgErrorNroRubrica = "No puedes ingresar caracteres";
                return false;
            }
            else
            {
                msgErrorNroRubrica = "";
            }

            // Fecha Rubrica
            if (libroP.FechaRubrica == null)
            {
                msgErrorFechaRubrica = "No seleccionaste la fecha correspondiente";
                return false;
            }
            else
            {
                msgErrorFechaRubrica = "";
            }

            // Folio Obra Transcripcion
            if (string.IsNullOrEmpty(libroP.FolioObraTranscripcion))
            {
                msgErrorFolio = "El campo no puede estar vacío";
                return false;
            }
            else
            {
                msgErrorFolio = "";
            }

            // FechaUltimaRegistracion
            if (libroP.FechaUltimaRegistracion == null)
            {
                msgErrorFechaUltimaRegistracion = "No seleccionaste la fecha correspondiente";
                return false;
            }
            else
            {
                msgErrorFechaUltimaRegistracion = "";
            }

            // Folio Ultima Registracion
            if (string.IsNullOrEmpty(libroP.FolioUltimaRegistracion))
            {
                msgErrormsgErrorFolioUltimaRegistracion = "El campo no puede estar vacío";
                return false;
            }
            else
            {
                msgErrormsgErrorFolioUltimaRegistracion = "";
            }

            // Si todos los campos pasan la validación o si algunos campos son "N/C", entonces retorna true
            return true;
        }
    }
}