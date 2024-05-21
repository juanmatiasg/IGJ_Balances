using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class ArchivosValidator : AbstractValidator<ArchivoDTO>
    {
        public ArchivosValidator()
        {
            RuleFor(_ => _.Categoria).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe seleccionar la categoria");

            RuleFor(_ => _.CantidadArchivos).Cascade(CascadeMode.Stop)
                           .GreaterThan(0).WithMessage("Debe adjuntar un archivo");

            RuleFor(_ => _.ContentType).Cascade(CascadeMode.Stop)
                           .Must(ValidaFormato).WithMessage("solo se aceptan archivos pdf");


        }
        private bool ValidaFormato(ArchivoDTO model, string formato)
        {

            if (model.ContentType != null)
            {


                if (formato.Contains("pdf")) return true;
            }


            return false;
        }
    }
}
