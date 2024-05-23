using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class CaratulaValidator : AbstractValidator<CaratulaDto>
    {
        public CaratulaValidator()
        {
            RuleFor(caratula => caratula.FechaInicio).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar la fecha de inicio");

            RuleFor(caratula => caratula.FechaDeCierre).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar la fecha de cierre")
                               .GreaterThan(_ => _.FechaInicio).WithMessage("la fecha de cierre de no puede ser menor que la fecha de inicio")
                               .NotEqual(_ => _.FechaInicio).WithMessage("La fecha del cierre no puede ser igual a la fecha de inicio");

            RuleFor(caratula => caratula.Email).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el mail")
                           .EmailAddress().WithMessage("Debe ingresar un mail valido");

            //instaciamos la validacion de la clase Entidad --Entidad Validator --
            RuleFor(caratula => caratula.Entidad).SetValidator(new EntidadValidator());

        }
    }
}
