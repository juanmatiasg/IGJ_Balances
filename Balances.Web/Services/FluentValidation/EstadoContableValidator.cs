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
            

            RuleFor(_ => _.inversionesActivoCorriente).Cascade(CascadeMode.Stop)
                   .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.activoCorrienteRestante).Cascade(CascadeMode.Stop)
                   .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.bienesDeUso).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");


            RuleFor(_ => _.propiedadesDeInversion).Cascade(CascadeMode.Stop)
              .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");


            RuleFor(_ => _.activoNoCorrienteRestante).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");


            RuleFor(_ => _.activoNoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.totalActivo).Cascade(CascadeMode.Stop)
              .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.deudorPasivoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.deudorPasivoNoCorriente).Cascade(CascadeMode.Stop)
               .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.deudorPasivoNoCorriente).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.pasivoNoCorriente).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");
            RuleFor(_ => _.totalPasivo).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.totalPasivo).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            //RUBROBALANCE

            RuleFor(_ => _.capitalSuscripto).Cascade(CascadeMode.Stop)
           .GreaterThan(0).WithMessage("El Capital suscripto debe ser 1 o mayor a 1");


            RuleFor(_ => _.aportesIrrevocables).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");

            RuleFor(_ => _.primaEmision).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");


            RuleFor(_ => _.reservaLegal).Cascade(CascadeMode.Stop)
           .GreaterThanOrEqualTo(0).WithMessage("No pueden ser valores negativos");


        }


    }
}
