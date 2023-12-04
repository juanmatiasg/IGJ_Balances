using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Utilities
{
    public static class ConfigManager
    {
        private const string BaseUrl = "https://localhost:7172/";

        public static class Endpoints
        {
            // Endpoint Busqueda Correlativo
            public static string BusquedaByCuilOrCorrelativo(string cuitCorrelativo) => $"{BaseUrl}BusquedaByCuilOrCorrelativo?cuitcorrelativo={cuitCorrelativo}";

            // Endpoint SendMail
            public static string SendMail(string email) => $"{BaseUrl}mail?email={email}";

            // Endpoint Balance
            public static string PostBalance => $"{BaseUrl}Balance/PostBalance";

            // Endpoint Session
            public static string SetSession(string balanceId) => $"{BaseUrl}Session?balanceId={balanceId}";
            public static string GetSession => $"{BaseUrl}Session";
        }

    }


}
