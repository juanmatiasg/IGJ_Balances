using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class AutoridadValidator : AbstractValidator<AutoridadDto>
    {
        public AutoridadValidator()
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

            RuleFor(_ => _.Cargo).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el cargo");


        }
    }
}
