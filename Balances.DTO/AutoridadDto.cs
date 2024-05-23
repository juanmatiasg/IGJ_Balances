namespace Balances.DTO
{
    public class AutoridadDto
    {


        public string SesionId { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NroDocumento { get; set; }
        public string? TipoDocumento { get; set; }
        public string NroFiscal { get; set; }

        public string Cargo { get; set; }

        public bool EsFirmante { get; set; }
        public bool EstaVigente { get; set; }

        //public BalanceDto Balance { get; set; }
    }
}
