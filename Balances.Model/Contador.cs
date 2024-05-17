namespace Balances.Model
{
    public class Contador
    {


        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NroDocumento { get; set; }

        public string TipoDocumento { get; set; }
        public string NroFiscal { get; set; }
        public string Tomo { get; set; }
        public string Folio { get; set; }
        public DateTime FechaInformeAuditorExt { get; set; }
        public string NroLegalInfoAudExt { get; set; }

        public string Observaciones { get; set; }
        public string Opinion { get; set; }
        public bool EsSocioEstudio { get; set; }
        public string TomoEstudio { get; set; }
        public string FolioEstudio { get; set; }


    }
}
