using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class PersonaJuridicaValidator : AbstractValidator<PersonaJuridicaDto>
    {
        public PersonaJuridicaValidator()
        {
            RuleFor(_ => _.Denominacion).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar la denominacion");

            RuleFor(_ => _.NroFiscal).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar el CUIL O CUIT");

            RuleFor(_ => _.Cuotas).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar la cantidad de cuotas");

            RuleFor(_ => _.Votos).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar la cantidad de votos");
        }
    }
}
