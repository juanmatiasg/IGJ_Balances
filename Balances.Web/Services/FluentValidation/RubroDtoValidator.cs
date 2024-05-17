using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class RubroDtoValidator : AbstractValidator<RubroPatrimonioNetoDto>
    {
        public RubroDtoValidator()
        {

            RuleFor(_ => _.denominacion).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Debe ingresar la denominación");

            RuleFor(_ => _.importe).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Debe ingresar el importe");


        }
    }
}
