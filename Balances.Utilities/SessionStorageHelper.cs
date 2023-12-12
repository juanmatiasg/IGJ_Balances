using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Balances.Utilities
{
    public  class SessionStorageHelper
    {
        private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();
        private static readonly Dictionary<DateTime, string> DateStorage = new Dictionary<DateTime, string>();

        private readonly IHttpContextAccessor _context=null;


        public SessionStorageHelper(IHttpContextAccessor context)
        {
                _context = context;
            
        }
        /// <summary>
        /// Seteo en Session el BALANCE ID con el que estamos trabajando
        /// </summary>
        /// <param name="balanceid">Identificador del balance que estamos cargando</param>
        public  void SetBalanceId(string balanceid )
        {
            
            var serializedValue = JsonSerializer.Serialize(balanceid);


            Storage[_context.HttpContext.Session.Id] = serializedValue;
            DateStorage[DateTime.UtcNow] = serializedValue; 

        }

        public  string GetBalanceId()
        {
            if (Storage.TryGetValue(_context.HttpContext.Session.Id, out var serializedValue))
            {
                return JsonSerializer.Deserialize<string>(serializedValue);
            }

            return default;
        }

    }
}
