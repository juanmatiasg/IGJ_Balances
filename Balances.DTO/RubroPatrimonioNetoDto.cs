using Balances.Model;

namespace Balances.DTO
{
    public class RubroPatrimonioNetoDto
    {
        public string codigo { get; set; }
        public string denominacion { get; set; }
        public decimal importe { get; set; }

        public RubroPatrimonioNetoDto() { }


        public RubroPatrimonioNetoDto(RubroPatrimonioNeto rubro)
        {

            denominacion = rubro.Denominacion;
            importe = rubro.Importe;
            codigo = rubro.Codigo;

        }

        public RubroPatrimonioNeto GetRubroPatrimonioNeto()
        {
            return new RubroPatrimonioNeto
            {
                Denominacion = denominacion,
                Importe = importe,
                Codigo = codigo
            };
        }

    }
}
