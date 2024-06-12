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
                               .NotEmpty().WithMessage("Debe ingresar el numero de documento")
                               .Matches(@"^\d+$").WithMessage("El número de documento no debe contener letras")
              .Must(NroDocumentoCoincideConCuit).When(_ => _.TipoDocumento == "DNI")
                 .WithMessage("El número de documento debe coincidir con el CUIL o CUIT");

            RuleFor(_ => _.NroFiscal).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el CUIL O CUIT")
                              .Matches(@"^[\d-]+$").WithMessage("El CUIT o CUIT no debe contener letras")
                                    .Must(EsCuitValido).WithMessage("El CUIL o CUIT no es válido");

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
            else if (!model.EsSocioEstudio && TomoEstudio is null or "")
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
            else if (!model.EsSocioEstudio && FolioEstudio is null or "")
            {

                return true;

            }



            return false;
        }

        public bool EsCuitValido(string cuit)
        {
            if (cuit.Contains("-"))
            {
                cuit = cuit.Replace("-", "");
            };

            if (string.IsNullOrEmpty(cuit) || cuit.Length != 11)
            {
                return false;
            }

            int[] multiplicadores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!int.TryParse(cuit[i].ToString(), out int digito))
                {
                    return false;
                }
                suma += digito * multiplicadores[i];
            }

            int resto = suma % 11;
            int digitoVerificador = resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;

            return int.TryParse(cuit[10].ToString(), out int ultimoDigito) && digitoVerificador == ultimoDigito;
        }

        private bool NroDocumentoCoincideConCuit(ContadorDto contador, string cuit)
        {
            if (contador.NroFiscal.Contains("-"))
            {
                contador.NroFiscal = contador.NroFiscal.Replace("-", "");
            };

            // Asegúrate de que el número de documento esté contenido dentro del CUIL/CUIT
            if (string.IsNullOrEmpty(contador.NroDocumento) || string.IsNullOrEmpty(contador.NroFiscal) || contador.NroFiscal.Length != 11)
            {
                return false;
            }

                string nroDocumentoEnCuit = contador.NroFiscal.Substring(2, 8); // Los 8 dígitos del documento empiezan en la posición 2 del CUIT
                return contador.NroDocumento == nroDocumentoEnCuit;
            
        }
    }
}

