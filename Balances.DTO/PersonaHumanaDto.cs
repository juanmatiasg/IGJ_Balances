namespace Balances.DTO
{
    public class PersonaHumanaDto
    {

        public string SesionId { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NroDocumento { get; set; }
        public string? TipoDocumento { get; set; }
        public string NroFiscal { get; set; }

        public string Cuotas { get; set; }
        public string Votos { get; set; }

        public bool EsFirmante { get; set; }
        public string ValorNominal { get; set; }
    }
}
