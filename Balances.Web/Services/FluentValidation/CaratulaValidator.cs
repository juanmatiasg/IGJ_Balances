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
                               .NotEmpty().WithMessage("Debe ingresar la fecha de cierre");


            RuleFor(caratula => caratula.Email).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el mail")
                           .EmailAddress().WithMessage("Debe ingresar un mail valido");

            //instaciamos la validacion de la clase Entidad --Entidad Validator --
            RuleFor(caratula => caratula.Entidad).SetValidator(new EntidadValidator());

        }
    }
}
