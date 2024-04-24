using Balances.Model;
using Balances.Services.Contract;
using Balances.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Balances.Services.Implementation
{
    public class SessionService : ISessionService
    {

        private readonly IHttpContextAccessor _context;
        private readonly ILogger<SessionService> _logger;


        private static readonly Dictionary<string, RegistroSesion> Storage = new Dictionary<string, RegistroSesion>();

        public SessionService(IHttpContextAccessor context, ILogger<SessionService> logger)
        {
            _context = context;
            _logger = logger;
        }



        public bool SetBalance(string sessionId, string balanceId)
        {


            RegistroSesion r = Storage[sessionId];

            if (r != null && balanceId != null)
            {
                r.RegistrarBalance(balanceId);
                return true;

            }
            return false;



        }



        /// <summary>
        /// Devuelve la nueva sesion creada
        /// </summary>
        /// <returns>Identificador de la Session Nueva Creada</returns>
        public string GetNewSesion()
        {
            try
            {
                RegistroSesion r = new RegistroSesion();
                Storage[r.Id] = r;

                return r.Id;

            }

            catch
            {
                return "";

            }

        }

        public string GetBalanceId(string sesionId)
        {
            RegistroSesion r = (RegistroSesion)Storage[sesionId];

            if (r != null)
            {
                return r.BalanceId;
            }

            return "";

        }

        public string GetSessionBalanceId()
        {
            var Sesionserializada = JsonSerializer.Serialize(Storage);
            var balanceID = SessionStorageHelper.GetBalanceId(Sesionserializada);

            return balanceID;

        }

        //public string GetSessionToken()
        //{

        //    var Sesionserializada = JsonSerializer.Serialize(Storage);
        //    var token = SessionStorageHelper.GetTokenKey(Sesionserializada);

        //    return token;
        //}




    }
}
