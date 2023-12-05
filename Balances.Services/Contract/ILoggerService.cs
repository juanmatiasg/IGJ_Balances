namespace Balances.Services.Contract
{
    public interface ILoggerService
    {
        void LogWarning(string message, Exception excepcion, object objeto);
        void LogError(string message, Exception excepcion, object objeto);
        void LogInfo(string message, Exception excepcion, object objeto);
    }
}
