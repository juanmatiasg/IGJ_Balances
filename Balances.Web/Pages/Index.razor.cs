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

namespace Balances.Web.Pages
{
    public partial class Index
    {
        private string msgError = "";
        private bool isChecked = false;
        public void handleAccept()
        {
            if (checkData())
            {
                NavigationManager.NavigateTo($"Caratula");
            }
        }

        private bool checkData()
        {
            if (!isChecked)
            {
                msgError = "Por favor acepta que has leído el marco legal";
                return false;
            }
            else
            {
                msgError = "";
            }

            msgError = "";
            return true;
        }
    }
}