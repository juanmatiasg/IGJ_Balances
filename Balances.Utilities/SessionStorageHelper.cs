namespace Balances.Utilities
{
    public static class SessionStorageHelper

    {


        public static string GetBalanceId(string session)
        {
            if (session == "{}")
            {
                return null;
            }

            var balanceId = new string(session.Reverse().Skip(2).Take(24).Reverse().ToArray());
            return balanceId;
        }

        public static string GetTokenKey(string session)
        {
            var token = new string(session.Skip(2).Take(36).ToArray());

            return token;
        }


        //private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();
        //private static readonly Dictionary<DateTime, string> DateStorage = new Dictionary<DateTime, string>();

        //private readonly IHttpContextAccessor _context=null;


        //public SessionStorageHelper(IHttpContextAccessor context)
        //{
        //        _context = context;

        //}
        ///// <summary>
        ///// Seteo en Session el BALANCE ID con el que estamos trabajando
        ///// </summary>
        ///// <param name="balanceid">Identificador del balance que estamos cargando</param>
        //public  void SetBalanceId(string balanceid )
        //{

        //    var serializedValue = JsonSerializer.Serialize(balanceid);


        //    Storage[_context.HttpContext.Session.Id] = serializedValue;
        //    DateStorage[DateTime.UtcNow] = serializedValue; 

        //}

        //public  string GetBalanceId()
        //{
        //    if (Storage.TryGetValue(_context.HttpContext.Session.Id, out var serializedValue))
        //    {
        //        return JsonSerializer.Deserialize<string>(serializedValue);
        //    }

        //    return default;
        //}

    }
}
