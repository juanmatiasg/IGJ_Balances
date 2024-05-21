using Balances.Model;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class EntidadValidator : AbstractValidator<Entidad>
    {
        public EntidadValidator()
        {

            RuleFor(_ => _.Correlativo).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el correlativo");

            RuleFor(_ => _.RazonSocial).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar la razon social");


            RuleFor(_ => _.TipoEntidad).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el tipo societario");


            RuleFor(_ => _.Domicilio).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe seleccionar el domicilio");

        }

    }
}
