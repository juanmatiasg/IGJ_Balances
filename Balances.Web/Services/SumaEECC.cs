using Balances.DTO;

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

            var total = estadoContable.bienesDeCambio + estadoContable.propiedadesDeInversion +
                  estadoContable.inversionesActivoNoCorriente +
                  estadoContable.inversionesActivoNoCorriente;


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
            if (estadoContable.totalActivo != 0 && estadoContable.totalPasivo != 0)
            {

                decimal rst = estadoContable.totalActivo - estadoContable.totalPasivo;
                return rst;
            }
            else
            {
                return 0;
            }
        }

        public static decimal OtrosRubrosPatrimonioNeto(EstadoContableDto estadoContableDto)
        {
            decimal SumaOtrosRubros = estadoContableDto.otrosRubros.Sum(_ => _.importe);

            var Total =
                estadoContableDto.ajusteCapital + estadoContableDto.capitalSuscripto
                + estadoContableDto.aportesIrrevocables + estadoContableDto.primaEmision +
                estadoContableDto.gananciasReservadas + estadoContableDto.perdidasAcumuladas +
                estadoContableDto.gananciasPerdidasEjercicio + estadoContableDto.reservaLegal
                + SumaOtrosRubros;

            return Total;

        }
    }
}
