namespace Balances.DTO
{
    public class LibroDto
    {

  
        public string Tipo { get; set; }
        public string Nombre { get; set; }

        public string NumeroRubrica { get; set; }

        public DateTime? FechaUltimaRegistracion { get; set; }
        public DateTime? FechaRubrica { get; set; }
        public string FolioObraTranscripcion { get; set; }
        public bool NoSabeNoContesta { get; set; }

        public string FolioUltimaRegistracion { get; set; }


    }

}
