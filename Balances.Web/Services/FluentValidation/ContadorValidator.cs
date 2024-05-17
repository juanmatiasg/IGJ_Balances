using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class ContadorValidator : AbstractValidator<ContadorDto>
    {
        public ContadorValidator()
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

            RuleFor(_ => _.NroLegalInfoAudExt).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar numero de legalizacion");

            RuleFor(_ => _.Tomo).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el tomo");


            RuleFor(_ => _.Folio).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el folio");


            RuleFor(_ => _.FechaInformeAuditorExt).Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Debe ingresar la fecha de informe");

            RuleFor(_ => _.Opinion).Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Debe ingresar la opinion del informe");


            RuleFor(_ => _.TomoEstudio).Cascade(CascadeMode.Stop)
                      .Must(ValidaTomoEstudio).WithMessage("Debe ingresar el tomo del estudio");


            RuleFor(_ => _.FolioEstudio).Cascade(CascadeMode.Stop)
                     .Must(ValidaFolioEstudio).WithMessage("Debe ingresar el folio del estudio");


        }

        private bool ValidaTomoEstudio(ContadorDto model, string TomoEstudio)
        {

            if (model.EsSocioEstudio && TomoEstudio is not null)
            {

                return true;

            }


            return false;
        }

        private bool ValidaFolioEstudio(ContadorDto model, string FolioEstudio)
        {

            if (model.EsSocioEstudio && FolioEstudio is not null)
            {

                return true;

            }


            return false;
        }
    }
}

