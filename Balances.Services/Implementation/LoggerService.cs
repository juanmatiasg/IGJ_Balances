using Balances.Services.Contract;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Balances.Services.Implementation
{
    public class LoggerService : ILoggerService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string ArchivoLog
        {
            get
            {
                return _webHostEnvironment.ContentRootPath + @"\log\" +
                    "Error-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
        }


        public LoggerService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void LogError(string message, Exception excepcion, object objeto)
        {
            string ArchLog = this.ArchivoLog;

            StreamWriter sw = new StreamWriter(this.ArchivoLog, true);
            string patron = "@fecha|ERROR|@mensaje|@excepcion|@parametro";
            string mensaje = patron.Replace("@fecha", DateTime.Now.ToLongDateString());
            mensaje = mensaje.Replace("@mensaje", message);
            mensaje = mensaje.Replace("@excepcion", excepcion.Message);
            mensaje = mensaje.Replace("@parametro", JsonConvert.SerializeObject(objeto));
            sw.WriteLine(message);
            sw.Close();
            //_logger.LogError(message, excepcion, objeto);
        }

        public void LogInfo(string message, Exception excepcion, object objeto)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, Exception excepcion, object objeto)
        {
            throw new NotImplementedException();
        }
    }
}
