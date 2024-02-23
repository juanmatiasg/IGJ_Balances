using Balances.Web.Services.Implementation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace Balances.Utilities
{
    public class StorageBalanceHelper : IStorageBalanceHelper
    {

      
        private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();
        private static readonly Dictionary<string, DateTime> Fecha = new Dictionary<string, DateTime>();
        private readonly IHttpContextAccessor _context=null;

        public StorageBalanceHelper(IHttpContextAccessor context)
        {
                _context = context;
            
        }
        /// <summary>
        /// Seteo en Session el BALANCE ID con el que estamos trabajando
        /// </summary>
        /// <param name="balanceid">Identificador del balance que estamos cargando</param>
        public  void SetBalanceId(string balanceid)
        {

            // var serializedValue = JsonSerializer.Serialize(balanceid);


            Storage[_context.HttpContext.Session.Id] = balanceid;//serializedValue;
            Fecha[_context.HttpContext.Session.Id] = DateTime.Now;
        }
       

        public string GetBalanceId()
        {

                
                if (Storage.TryGetValue(_context.HttpContext.Session.Id, out var balanceid))
                {
                    return balanceid;
                }
                else
                    return "";

        }



    }
}
