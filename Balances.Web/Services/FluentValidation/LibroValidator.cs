using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class LibroDtoExtendedValidator : AbstractValidator<LibroDtoExtended>
    {
        public LibroDtoExtendedValidator()
        {
            RuleFor(_ => _.Tipo).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el tipo de libro");

            RuleFor(_ => _.Nombre).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el nombre");

            RuleFor(_ => _.NumeroRubrica).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el numero de rubrica");

            RuleFor(_ => _.FolioObraTranscripcion).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el folio");

            RuleFor(_ => _.FolioUltimaRegistracion).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el folio de la ultima registracion");


            RuleFor(_ => _.FechaRubrica).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar la fecha de rubrico");

            RuleFor(_ => _.FechaUltimaRegistracion).Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Debe ingresar la fecha de la ultima registracion");


        }
    }
}
