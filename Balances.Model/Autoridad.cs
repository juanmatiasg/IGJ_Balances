namespace Balances.Model
{
    public class Autoridad
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NroFiscal { get; set; }

        public string Cargo { get; set; }

        public bool EsFirmante { get; set; }
        public bool EstaVigente { get; set; }
        //public string Estado { get; internal set; }
    }
}
