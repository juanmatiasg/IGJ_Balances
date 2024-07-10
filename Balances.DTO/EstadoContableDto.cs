using Balances.Model;

namespace Balances.DTO
{
    public class EstadoContableDto
    {


        public string SesionId { get; set; }
        public string tipoBalance { get; set; }
        public DateTime? fechaEstado { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaReunionDirectorio { get; set; }
        public DateTime? fechaAsamblea { get; set; }


        public decimal cajaYBancos { get; set; }
        public decimal inversionesActivoCorriente { get; set; }
        public decimal bienesDeCambio { get; set; }
        public decimal activoCorrienteRestante { get; set; }
        public decimal activoCorriente { get; set; }




        public decimal bienesDeUso { get; set; }
        public decimal propiedadesDeInversion { get; set; }
        public decimal inversionesActivoNoCorriente { get; set; }
        public decimal activoNoCorrienteRestante { get; set; }
        public decimal activoNoCorriente { get; set; }



        public decimal totalActivo { get; set; }

        public decimal deudorPasivoCorriente { get; set; }
        public decimal pasivoCorriente { get; set; }



        public decimal deudorPasivoNoCorriente { get; set; }
        public decimal pasivoNoCorriente { get; set; }
        public decimal totalPasivo { get; set; }
        public decimal patrimonioNeto { get; set; }

        public decimal capitalSuscripto { get; set; }
        public decimal ajusteCapital { get; set; }
        public decimal aportesIrrevocables { get; set; }
        public decimal primaEmision { get; set; }
        public decimal resultadosEjercicio { get; set; }
        public decimal gananciasPerdidasInicioEjercicio { get; set; }
        public decimal reservaLegal { get; set; }

        public decimal totalRubro { get; set; }



        private List<RubroPatrimonioNetoDto> _otrosRubros;
        public List<RubroPatrimonioNetoDto> otrosRubros
        {
            get
            {
                // Instanciar la lista si aún no ha sido inicializada
                if (_otrosRubros == null)
                {
                    _otrosRubros = new List<RubroPatrimonioNetoDto>();
                }
                return _otrosRubros;
            }
            set { _otrosRubros = value; }
        }



        public EstadoContableDto()
        {
        }

        public EstadoContableDto(EstadoContable a)
        {

            tipoBalance = a.TipoBalance;
            fechaEstado = a.FechaEstado;
            fechaInicio = a.FechaInicio;
            fechaReunionDirectorio = a.FechaReunionDirectorio;
            fechaAsamblea = a.FechaAsamblea;


            cajaYBancos = a.CajaYBancos;
            inversionesActivoCorriente = a.InversionesActivoCorriente;
            bienesDeCambio = a.BienesDeCambio;
            activoCorrienteRestante = a.ActivoCorrienteRestante;

            activoCorriente = a.ActivoCorriente;



            bienesDeUso = a.BienesDeUso;
            propiedadesDeInversion = a.PropiedadesDeInversion;
            inversionesActivoNoCorriente = a.InversionesActivoNoCorriente;
            activoNoCorrienteRestante = a.ActivoNoCorrienteRestante;
            activoNoCorriente = a.ActivoNoCorriente;



            totalActivo = a.TotalActivo;

            deudorPasivoCorriente = (decimal)a.DeudorPasivoCorriente;
            pasivoCorriente = (decimal)a.PasivoCorriente;

            deudorPasivoNoCorriente = (decimal)a.DeudorPasivoNoCorriente;
            pasivoNoCorriente = (decimal)a.PasivoNoCorriente;


            totalPasivo = (decimal)a.TotalPasivo;
            patrimonioNeto = (decimal)a.PatrimonioNeto;
            capitalSuscripto = (decimal)a.CapitalSuscripto;
            ajusteCapital = (decimal)a.AjusteCapital;
            aportesIrrevocables = (decimal)a.AportesIrrevocables;
            primaEmision = (decimal)a.PrimaEmision;
            resultadosEjercicio = (decimal)a.resultadosEjercicio;
            gananciasPerdidasInicioEjercicio = (decimal)a.gananciasPerdidasInicioEjercicio;
            reservaLegal = (decimal)a.ReservaLegal;
            otrosRubros = ConvertirARubroPatrimonioNetoDto(a.OtrosRubros);
        }

        public static List<RubroPatrimonioNetoDto> ConvertirARubroPatrimonioNetoDto(List<RubroPatrimonioNeto> lista)
        {
            List<RubroPatrimonioNetoDto> nuevaLista = new List<RubroPatrimonioNetoDto>();


            foreach (var item in lista)
            {

                RubroPatrimonioNetoDto nuevoItem = new RubroPatrimonioNetoDto();


                nuevoItem.codigo = item.Codigo;
                nuevoItem.denominacion = item.Denominacion;
                nuevoItem.importe = item.Importe;

                nuevaLista.Add(nuevoItem);



            }
            return nuevaLista;
        }






    }
}
