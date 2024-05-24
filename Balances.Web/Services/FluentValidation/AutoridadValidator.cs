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
                               .NotEmpty().WithMessage("Debe ingresar el numero de documento")
             .Matches(@"^\d+$").WithMessage("El número de documento no debe contener letras")
              .Must(NroDocumentoCoincideConCuit).When(_ => _.TipoDocumento == "DNI")
                 .WithMessage("El número de documento debe coincidir con el CUIL o CUIT");

            RuleFor(_ => _.NroFiscal).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Debe ingresar el CUIL O CUIT")
                              .Matches(@"^\d+$").WithMessage("El CUIT o CUIT no debe contener letras")
                                    .Must(EsCuitValido).WithMessage("El CUIL o CUIT no es válido");

            RuleFor(_ => _.Cargo).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar el cargo");



        }

        public bool EsCuitValido(string cuit)
        {
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

        private bool NroDocumentoCoincideConCuit(AutoridadDto autoridad, string cuit)
        {

            // Asegúrate de que el número de documento esté contenido dentro del CUIL/CUIT
            if (string.IsNullOrEmpty(autoridad.NroDocumento) || string.IsNullOrEmpty(autoridad.NroFiscal) || autoridad.NroFiscal.Length != 11)
            {
                return false;
            }

            string nroDocumentoEnCuit = autoridad.NroFiscal.Substring(2, 8); // Los 8 dígitos del documento empiezan en la posición 2 del CUIT
            return autoridad.NroDocumento == nroDocumentoEnCuit;
        }



    }

}








