using Balances.DTO;
using FluentValidation;

namespace Balances.Web.Services.FluentValidation
{
    public class PersonaJuridicaValidator : AbstractValidator<PersonaJuridicaDto>
    {
        public PersonaJuridicaValidator()
        {
            RuleFor(_ => _.Denominacion).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar Razón Social");

            RuleFor(_ => _.Pais).Cascade(CascadeMode.Stop)
                         .NotEmpty().WithMessage("Debe ingresar País");


            RuleFor(_ => _.NroFiscal).Cascade(CascadeMode.Stop)
                .Matches(@"^[\d-]+$").WithMessage("El Cuit no debe contener letras")
                .NotEmpty().WithMessage("Debe ingresar el CUIL O CUIT").
                Must(EsCuitValido).WithMessage("El CUIT no es válido");


            RuleFor(_ => _.Cuotas).Cascade(CascadeMode.Stop)
                .Matches(@"^\d+$")
                .WithMessage("La cantidad de cuotas no debe contener letras")
                .NotEmpty().WithMessage("Debe ingresar la cantidad de cuotas");



            RuleFor(_ => _.Votos).Cascade(CascadeMode.Stop).Matches(@"^\d+$")
                .WithMessage("La cantidad de votos no debe contener letras")
                .NotEmpty().WithMessage("Debe ingresar la cantidad de votos");

            RuleFor(_ => _.ValorNominal).Cascade(CascadeMode.Stop)
                             .NotEmpty().WithMessage("Debe ingresar el Valor nominal")
                             .Matches(@"^\d+$").WithMessage("El valor nominal no puede contener letras");
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
    }
}
