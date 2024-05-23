using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class BusquedaEntidadValidator : AbstractValidator<BusquedaEntidadRequest>
    {
        public BusquedaEntidadValidator()
        {
            RuleFor(_ => _.NroCorrelativoNroCUIL).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el correlativo").
                                Matches(@"^\d+$").WithMessage("El correlativo solo puede contener números"); ;

        }
    }
}
