namespace Balances.Model
{
    public class EstadoContable
    {
        public string TipoBalance { get; set; }
        public DateTime FechaEstado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaReunionDirectorio { get; set; }
        public DateTime FechaAsamblea { get; set; }



        public decimal CajaYBancos { get; set; }
        public decimal InversionesActivoCorriente { get; set; }
        public decimal BienesDeCambio { get; set; }
        public decimal ActivoCorrienteRestante { get; set; }


        public decimal ActivoCorriente { get; set; }


        public decimal BienesDeUso { get; set; }
        public decimal PropiedadesDeInversion { get; set; }
        public decimal InversionesActivoNoCorriente { get; set; }
        public decimal ActivoNoCorrienteRestante { get; set; }



        public decimal ActivoNoCorriente { get; set; }

        public decimal TotalActivo { get; set; }

        public decimal? DeudorPasivoCorriente { get; set; }
        public decimal? PasivoCorriente { get; set; }

        public decimal? DeudorPasivoNoCorriente { get; set; }
        public decimal? PasivoNoCorriente { get; set; }
        public decimal? TotalPasivo { get; set; }
        public decimal? PatrimonioNeto { get; set; }

        public decimal? CapitalSuscripto { get; set; }
        public decimal? AjusteCapital { get; set; }
        public decimal? AportesIrrevocables { get; set; }
        public decimal? PrimaEmision { get; set; }
        public decimal? GananciasReservadas { get; set; }
        public decimal? PerdidasAcumuladas { get; set; }
        public decimal? GananciasPerdidasEjercicio { get; set; }
        public decimal? ReservaLegal { get; set; }

        public List<RubroPatrimonioNeto> otrosRubros { get; set; }
        //public string Estado { get; set; }
        //public string OtrosRubrosEstado { get; internal set; }
    }
}
