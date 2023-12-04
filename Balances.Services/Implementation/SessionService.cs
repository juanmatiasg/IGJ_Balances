using Balances.Services.Contract;
using Microsoft.AspNetCore.Http;
using System.Text;


namespace Balances.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _context;

        public string BalanceId
        {
            get { return GetBalanceId(); }
            set { SetBalanceId(value); }
        }

        public SessionService(IHttpContextAccessor context)
        {
            _context = context;
        }


        public void SetBalanceId(string balanceId)
        {
            _context.HttpContext.Session.SetString("idSession", balanceId);
        }

        public string GetBalanceId()
        {

            return _context.HttpContext.Session.GetString("idSession");

        }
    }
}
