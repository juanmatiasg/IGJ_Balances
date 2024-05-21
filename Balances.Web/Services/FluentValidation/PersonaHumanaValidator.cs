using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class PersonaHumanaValidator : AbstractValidator<PersonaHumanaDto>
    {
        public PersonaHumanaValidator()
        {
            RuleFor(_ => _.Nombre).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el nombre");

            RuleFor(_ => _.Apellido).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el Apellido");

            RuleFor(_ => _.TipoDocumento).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe seleccionar el tipo de documento");

            RuleFor(_ => _.NroDocumento).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el numero de documento");
            RuleFor(_ => _.NroFiscal).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar el CUIL O CUIT");

            RuleFor(_ => _.Cuotas).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar la cantidad de cuotas");

            RuleFor(_ => _.Votos).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar la cantidad de votos");

        }
    }
}
