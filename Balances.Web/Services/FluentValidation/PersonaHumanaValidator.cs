using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class PersonaHumanaValidator : AbstractValidator<PersonaHumanaDto>
    {
        public PersonaHumanaValidator()
        {

            RuleFor(_ => _.Nombre).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el Nombre")
                               .Matches(@"^[^\d]+$").WithMessage("El Nombre no debe contener números");


            RuleFor(_ => _.Apellido).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe ingresar el Apellido")
                .Matches(@"^[^\d]+$").WithMessage("El Apellido no debe contener números");



            RuleFor(_ => _.TipoDocumento).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe seleccionar el Tipo de documento");

            RuleFor(_ => _.NroDocumento).Cascade(CascadeMode.Stop)
                               .NotEmpty().WithMessage("Debe ingresar el número de documento")
                               .Matches(@"^\d+$").WithMessage("El número de documento no debe contener letras");

            RuleFor(_ => _.NroFiscal)
                                    .Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Debe ingresar el CUIL o CUIT")
                                    .Matches(@"^\d+$").WithMessage("El CUIT o CUIT no debe contener letras")
                                    .Must(EsCuitValido).WithMessage("El CUIL o CUIT no es válido");


            _ = RuleFor(_ => _.NroDocumento)
                  .Must(NroDocumentoCoincideConCuit).When(_ => _.TipoDocumento == "DNI")
                  .WithMessage("El número de documento debe coincidir con el CUIL o CUIT");

            RuleFor(_ => _.Cuotas).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar la cantidad de cuotas")
                                    .Matches(@"^\d+$").WithMessage("La cantidad de cuotas no debe contener letras");

            RuleFor(_ => _.Votos).Cascade(CascadeMode.Stop)
                           .NotEmpty().WithMessage("Debe ingresar la cantidad de votos")
                            .Matches(@"^\d+$").WithMessage("La cantidad de votos no debe contener letras"); ;

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

        private bool NroDocumentoCoincideConCuit(PersonaHumanaDto persona , string arg)
        {
           
            // Asegúrate de que el número de documento esté contenido dentro del CUIL/CUIT
            if (string.IsNullOrEmpty(persona.NroDocumento) || string.IsNullOrEmpty(persona.NroFiscal) || persona.NroFiscal.Length != 11)
            {
                return false;
            }

            string nroDocumentoEnCuit = persona.NroFiscal.Substring(2, 8); // Los 8 dígitos del documento empiezan en la posición 2 del CUIT
            return persona.NroDocumento == nroDocumentoEnCuit;
        }





    }
}