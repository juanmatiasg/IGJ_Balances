namespace Balances.Model
{
    public class RepresentanteLegal
    {
        public string Id { get; set; }
        public string RepresentanteNombre { get; set; }
        public string RepresentanteApellido { get; set; }

        public string RepresentanteNroDocumento { get; set; }
        public string RepresentanteTipoDocumento { get; set; }
        public string RepresentanteNroFiscal { get; set; }

        public string RepresentanteCargo { get; set; }

        public bool EsFirmante { get; set; }
        public string Estado { get; internal set; }

    }
}
