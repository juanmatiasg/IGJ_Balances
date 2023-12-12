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

        // Diccionario para almacenar balanceId junto con la fecha actual
        private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();

        private const string KEY_SESSION = "idSession";

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
            _logger.LogWarning($"Método SetBalanceId invocado id: \n {balanceId} {JsonConvert.SerializeObject(new BalanceDto())}");
            _logger.LogError($"Invocando el logError en SetBalanceId {balanceId}");

            // Obtener la fecha actual
            DateTime currentDate = DateTime.Now;

            // Almacenar el balanceId en el diccionario junto con la fecha actual
            string key = currentDate.ToString("yyyyMMddHHmmss");
            Storage[key] = balanceId;

            // También puedes almacenar el balanceId en la sesión si lo necesitas por separado
            _context.HttpContext.Session.SetString(KEY_SESSION, balanceId);
        }

        public string GetBalanceId()
        {
            try
            {
                // Verificar si hay claves en el diccionario antes de intentar obtener el valor máximo
                if (Storage.Keys.Any())
                {
                    // Obtener la fecha más reciente en el diccionario
                    string mostRecentKey = Storage.Keys.Max();

                    // Obtener el balanceId correspondiente a la fecha más reciente
                    if (Storage.TryGetValue(mostRecentKey, out string balanceId))
                    {
                        return balanceId;
                    }
                }

                // Manejar el caso donde no se encontró el balanceId correspondiente a la fecha más reciente
                _logger.LogWarning("No se encontró balanceId correspondiente a la fecha más reciente.");
                return null;
            }
            catch (Exception ex)
            {
                // Propagar la excepción
                _logger.LogError($"Error al obtener el balanceId: {ex.Message}");
                throw;
            }
        }
    }
}
