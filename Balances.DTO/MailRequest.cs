using Balances.Model;

namespace Dominio.Helpers
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
        public List<Archivo> Archivos { get; set; }
        public Archivo archivo { get; set; }
    }
}
