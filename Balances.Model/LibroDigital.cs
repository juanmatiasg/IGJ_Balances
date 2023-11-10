namespace Balances.Model
{
    public class LibroDigital
    {
        public string TipoDocumento { get; set; }
        public string Denominacion { get; set; }

        public string Nombre { get; set; }
        public string NumeroRl { get; set; }
        public DateTime FechaUltimaRegistracion { get; set; }
        public string Folio { get; set; }
        public bool NoSabeNoContesta { get; set; }
    }
}
