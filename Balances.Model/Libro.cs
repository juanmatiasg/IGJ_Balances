namespace Balances.Model
{
    public class Libro
    {
        public string TipoDocumento { get; set; }
        //public string Denominacion { get; set; }
        public string Nombre { get; set; }
        public string NumeroRubrica { get; set; }
        public DateTime? FechaRubrica { get; set; }
        public DateTime? FechaUltimaRegistracion { get; set; }
        public string FolioObraTranscripcion { get; set; }
        public string FolioUltimaRegistracion { get; set; }
        public bool NoSabeNoContesta { get; set; }
    }
}
