using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class EstadoContableValidator : AbstractValidator<EstadoContableDto>
    {
        public EstadoContableValidator()
        {
            //PERIODO
            RuleFor(_ => _.tipoBalance).Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("Debe ingresar el tipo de balance");

            RuleFor(_ => _.fechaInicio).Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("Debe ingresar la fecha de incio");

            //APROBADO POR
            RuleFor(_ => _.fechaReunionDirectorio).Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("Debe ingresar la fecha de reunion autoridades")
                            .GreaterThan(_ => _.fechaEstado)
                            .WithMessage("la fecha de reunion de autoridades no puede ser anterior a la fecha de cierre del estado contable");

            RuleFor(_ => _.fechaAsamblea).Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Debe ingresar la fecha de reunion de socios")
                    .GreaterThan(_ => _.fechaEstado)
                    .WithMessage("la fecha de reunion de socios no puede ser anterior a la fecha de cierre del estado contable");


            //RUBROBALANCE
            RuleFor(_ => _.cajaYBancos).Cascade(CascadeMode.Stop)
                     .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de caja");

            RuleFor(_ => _.inversionesActivoCorriente).Cascade(CascadeMode.Stop)
                   .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de inversiones");

            RuleFor(_ => _.activoCorrienteRestante).Cascade(CascadeMode.Stop)
                   .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del activo corriente restante");

            RuleFor(_ => _.bienesDeUso).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de los bienes de uso");


            RuleFor(_ => _.propiedadesDeInversion).Cascade(CascadeMode.Stop)
              .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de las propiedades de inversion");


            RuleFor(_ => _.activoNoCorrienteRestante).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del activo no corriente restante");


            RuleFor(_ => _.activoNoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del activo no corriente corriente");

            RuleFor(_ => _.totalActivo).Cascade(CascadeMode.Stop)
              .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del total activo");

            RuleFor(_ => _.deudorPasivoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de deudas del pasivo corriente");

            RuleFor(_ => _.deudorPasivoNoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de deudas del pasivo no corriente");

            RuleFor(_ => _.deudorPasivoNoCorriente).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto de deudas del pasivo no corriente");

            RuleFor(_ => _.pasivoNoCorriente).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del pasivo no coriente");
            RuleFor(_ => _.totalPasivo).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del total pasivo");

            RuleFor(_ => _.totalPasivo).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el monto del patrimonio neto");

            //RUBROBALANCE

            RuleFor(_ => _.capitalSuscripto).Cascade(CascadeMode.Stop)
           .GreaterThan(0).WithMessage("Debe ingresar el capital suscripto");


            RuleFor(_ => _.ajusteCapital).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar el ajuste del capital");

            RuleFor(_ => _.aportesIrrevocables).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar los aportes irrevocables");

            RuleFor(_ => _.primaEmision).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar la prima de emision");

            RuleFor(_ => _.gananciasReservadas).Cascade(CascadeMode.Stop)
          .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar las ganancias reservadas");

            RuleFor(_ => _.perdidasAcumuladas).Cascade(CascadeMode.Stop)
           .LessThanOrEqualTo(0).WithMessage("Debe ingresar las perdidas acumuladas");

           // RuleFor(_ => _.gananciasPerdidasEjercicio).Cascade(CascadeMode.Stop)
           //.GreaterThanOrEqualTo(0).WithMessage("Debe ingresar las ganancias/perdidas del ejercicio");

            RuleFor(_ => _.reservaLegal).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("Debe ingresar la reserva legal");


        }


    }
}
