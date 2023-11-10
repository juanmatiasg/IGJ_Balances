using Balances.Model;

namespace Balances.DTO
{
    public class EstadoContableDto
    {
        public string tipoBalance { get; set; }
        public DateTime fechaEstado { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaReunionDirectorio { get; set; }
        public DateTime fechaAsamblea { get; set; }


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
        public decimal gananciasReservadas { get; set; }
        public decimal perdidasAcumuladas { get; set; }
        public decimal gananciasPerdidasEjercicio { get; set; }
        public decimal reservaLegal { get; set; }

        public RubrosPatrimonioNetoDto? otrosRubros { get; set; }

        public EstadoContable GetEstadoContable()
        {
            var estado = new EstadoContable
            {
                TipoBalance = tipoBalance,
                FechaEstado = fechaEstado,
                FechaInicio = fechaInicio,
                FechaReunionDirectorio = (DateTime)fechaReunionDirectorio,
                FechaAsamblea = fechaAsamblea,

                CajaYBancos = cajaYBancos,
                InversionesActivoCorriente = inversionesActivoCorriente,
                BienesDeCambio = bienesDeCambio,
                ActivoCorrienteRestante = activoCorrienteRestante,
                ActivoCorriente = activoCorriente,

                BienesDeUso = bienesDeUso,
                PropiedadesDeInversion = propiedadesDeInversion,
                InversionesActivoNoCorriente = inversionesActivoNoCorriente,
                ActivoNoCorrienteRestante = activoNoCorrienteRestante,
                ActivoNoCorriente = activoNoCorriente,

                TotalActivo = totalActivo,

                DeudorPasivoCorriente = deudorPasivoCorriente,
                PasivoCorriente = pasivoCorriente,

                DeudorPasivoNoCorriente = deudorPasivoNoCorriente,
                PasivoNoCorriente = pasivoNoCorriente,

                TotalPasivo = totalPasivo,
                PatrimonioNeto = patrimonioNeto,
                CapitalSuscripto = capitalSuscripto,
                AjusteCapital = ajusteCapital,
                AportesIrrevocables = aportesIrrevocables,
                PrimaEmision = primaEmision,
                GananciasReservadas = gananciasReservadas,
                PerdidasAcumuladas = perdidasAcumuladas,
                GananciasPerdidasEjercicio = gananciasPerdidasEjercicio,
                ReservaLegal = reservaLegal

            };


            estado.OtrosRubros = otrosRubros.GetRubrosPatrimonioNeto();



            return estado;
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
            gananciasReservadas = (decimal)a.GananciasReservadas;
            perdidasAcumuladas = (decimal)a.PerdidasAcumuladas;
            gananciasPerdidasEjercicio = (decimal)a.GananciasPerdidasEjercicio;
            reservaLegal = (decimal)a.ReservaLegal;

            otrosRubros = new RubrosPatrimonioNetoDto(a.OtrosRubros);

        }

        public EstadoContableDto()
        {
        }
    }
}
