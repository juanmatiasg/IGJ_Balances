namespace Balances.Model
{
    public class Contador
    {
        public string TipoDocumento;

        public string ContadorNombre { get; set; }
        public string ContadorApellido { get; set; }

        public string ContadorNroDocumento { get; set; }
        public string ContadorNroFiscal { get; set; }
        public string Tomo { get; set; }
        public string Folio { get; set; }
        public DateTime FechaInformeAuditorExt { get; set; }
        public string NroLegalInfoAudExt { get; set; }
        //public string Estado { get; internal set; }
    }
}
