namespace Balances.DTO
{
    public class EntidadRequestDTO
    {
        public string RazonSocial { get; set; }
        public string TipoEntidad { get; set; }
        public string Domicilio { get; set; }
        public bool SedeSocialInscripta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaDeCierre { get; set; }
    }
}
