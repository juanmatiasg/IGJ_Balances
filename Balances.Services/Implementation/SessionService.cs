using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;


namespace Balances.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _context;
        private readonly ILogger<SessionService> _logger;

        public string BalanceId
        {
            get { return GetBalanceId(); }
            set { SetBalanceId(value); }
        }

        public SessionService(IHttpContextAccessor context, ILogger<SessionService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public void SetBalanceId(string balanceId)
        {
            _logger.LogWarning($"Metodo SetBalanceId invocado id: \n {balanceId} {JsonConvert.SerializeObject(new BalanceDto())}");

            _logger.LogError($"Invocando el logError en SetBalanceId {balanceId}");
            _context.HttpContext.Session.SetString("idSession", balanceId);
        }

        public string GetBalanceId()
        {

            return _context.HttpContext.Session.GetString("idSession");

        }


    }
}
