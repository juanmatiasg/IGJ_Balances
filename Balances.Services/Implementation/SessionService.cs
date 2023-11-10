using Balances.Services.Contract;
using Microsoft.AspNetCore.Http;
using System.Text;


namespace Balances.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _context;

        public SessionService(IHttpContextAccessor context)
        {
            _context = context;
        }

        
         public string CreateSessionId(string key ,string value)
         {
             _context.HttpContext.Session.SetString(key, value);

            return value;
         }

         public string GetSessionId(string key)
         {
             return _context.HttpContext.Session.GetString(key);

         }


    }
}
