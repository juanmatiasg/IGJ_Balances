﻿using Balances.Services.Contract;
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


        private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();

        public SessionService(IHttpContextAccessor context, ILogger<SessionService> logger)
        {
            _context = context;
            _logger = logger;
        }



        public void SetSession(string balanceId)
        {


            Storage[Token.KEY_SESSION] = balanceId;
            _context.HttpContext.Session.SetString(Token.KEY_SESSION, balanceId);

        }

        public Dictionary<string, string> GetSession()
        {
            try
            {

                return Storage;

            }

            catch (Exception ex)
            {
                return null;

            }

        }

        public string GetSessionBalanceId()
        {
            var Sesionserializada = JsonSerializer.Serialize(Storage);
            var balanceID = SessionStorageHelper.GetBalanceId(Sesionserializada);

            return balanceID;

        }

        public string GetSessionToken()
        {

            var Sesionserializada = JsonSerializer.Serialize(Storage);
            var token = SessionStorageHelper.GetTokenKey(Sesionserializada);

            return token;
        }
        //public void SetBalanceId(string balanceId)
        //{
        //    _logger.LogWarning($"Método SetBalanceId invocado id: \n {balanceId} {JsonConvert.SerializeObject(new BalanceDto())}");
        //    _logger.LogError($"Invocando el logError en SetBalanceId {balanceId}");




        //    Storage[Token.KEY_SESSION] = balanceId;

        //    // También puedes almacenar el balanceId en la sesión si lo necesitas por separado
        //    _context.HttpContext.Session.SetString(Token.KEY_SESSION, balanceId);
        //}


        //public void SetBalanceId(string balanceId, string token)
        //{
        //    //_logger.LogWarning($"Método SetBalanceId invocado id: \n {balanceId} {JsonConvert.SerializeObject(new BalanceDto())}");
        //    //_logger.LogError($"Invocando el logError en SetBalanceId {balanceId}");



        //    Storage[Token.KEY_SESSION] = balanceId;


        //    // También puedes almacenar el balanceId en la sesión si lo necesitas por separado
        //    _context.HttpContext.Session.SetString(token, balanceId);
        //}

        //public string GetToken()
        //{
        //    try
        //    {
        //        // Verificar si hay claves en el diccionario
        //        if (Storage.Keys.Any())
        //        {

        //            return Storage[Token.KEY_SESSION];
        //        }

        //        // Manejar el caso donde no se encontró el balanceId correspondiente a la fecha más reciente
        //        _logger.LogWarning("No se encontró balanceId correspondiente");
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Propagar la excepción
        //        _logger.LogError($"Error al obtener el balanceId: {ex.Message}");
        //        throw;
        //    }
        //}

    }
}
