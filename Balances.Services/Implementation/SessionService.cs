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

        
         public void CreateSessionId(string balanceId)
         {
             _context.HttpContext.Session.SetString("idSession", balanceId);
         }

         public string GetSessionId()
         {

             return _context.HttpContext.Session.GetString("idSession");

         }


    }
}
