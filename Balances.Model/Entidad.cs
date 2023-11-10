namespace Balances.Model
{
    public class Entidad
    {
        public string RazonSocial { get; set; }

        public string TipoEntidad { get; set; }

        public string Domicilio { get; set; }

        public string Correlativo { get; set; }

        public bool SedeSocialInscripta { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaDeCierre { get; set; }

    }
}