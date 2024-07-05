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
        public decimal resultadosEjercicio { get; set; }
        public decimal gananciasPerdidasInicioEjercicio { get; set; }
        public decimal? ReservaLegal { get; set; }

        public decimal totalRubro { get; set; }


        private List<RubroPatrimonioNeto> _OtrosRubros;
        public List<RubroPatrimonioNeto> OtrosRubros
        {
            get
            {
                // Instanciar la lista si aún no ha sido inicializada
                if (_OtrosRubros == null)
                {
                    _OtrosRubros = new List<RubroPatrimonioNeto>();
                }
                return _OtrosRubros;
            }
            set { _OtrosRubros = value; }
        }


        public EstadoContable(){
         
        }
    }


}
