using Balances.DTO;
using Balances.Model;

namespace Balances.Web.Services
{
    public static class SumaEECC
    {
        public static decimal ActivoCorriente(EstadoContableDto estadoContable)
        {
            var total = estadoContable.cajaYBancos + estadoContable.inversionesActivoCorriente +
                        estadoContable.bienesDeCambio + estadoContable.activoCorrienteRestante;

            return total;
        }

        public static decimal ActivoNoCorriente(EstadoContableDto estadoContable)
        {

            var total = estadoContable.bienesDeUso + estadoContable.propiedadesDeInversion +
                  estadoContable.inversionesActivoNoCorriente +
                  estadoContable.activoNoCorrienteRestante;


            return total;



        }

        public static decimal TotalActivo(EstadoContableDto estadoContable)
        {
            var total = estadoContable.activoCorriente + estadoContable.activoNoCorriente;
            return total;
        }

        public static decimal TotalPasivo(EstadoContableDto estadoContable)
        {
            var total = estadoContable.deudorPasivoCorriente + estadoContable.deudorPasivoNoCorriente;

            return total;
        }

        public static decimal PatrimonioNeto(EstadoContableDto estadoContable)
        {
                decimal rst = estadoContable.totalActivo - estadoContable.totalPasivo;
                return rst;
        }

        public static decimal PatrimonioNeto(EstadoContable estadoContable)
        {
                decimal rst = (decimal)(estadoContable.TotalActivo - estadoContable.TotalPasivo);
                return rst;
        }

        public static decimal OtrosRubrosPatrimonioNeto(EstadoContableDto estadoContableDto)
        {
            decimal SumaOtrosRubros = estadoContableDto.otrosRubros.Sum(_ => _.importe);

            var Total =
                estadoContableDto.ajusteCapital + estadoContableDto.capitalSuscripto
                + estadoContableDto.aportesIrrevocables + estadoContableDto.primaEmision +
                estadoContableDto.resultadosEjercicio+
                estadoContableDto.gananciasPerdidasInicioEjercicio + estadoContableDto.reservaLegal
                + SumaOtrosRubros;

            return Total;

        }

        public static decimal OtrosRubrosPatrimonioNeto(EstadoContable estadoContable)
        {
            decimal SumaOtrosRubros = estadoContable.OtrosRubros.Sum(_ => _.Importe);

            var Total =
                estadoContable.AjusteCapital + estadoContable.CapitalSuscripto
                + estadoContable.AportesIrrevocables + estadoContable.PrimaEmision +
                estadoContable.resultadosEjercicio +
                estadoContable.gananciasPerdidasInicioEjercicio + estadoContable.ReservaLegal
                + SumaOtrosRubros;

            return (decimal)Total;

        }
    }
}
