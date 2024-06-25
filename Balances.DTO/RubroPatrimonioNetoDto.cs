namespace Balances.DTO
{
    public class RubroPatrimonioNetoDto
    {

        public string SesionId { get; set; }
        public string codigo { get; set; } = Guid.NewGuid().ToString();
        public string denominacion { get; set; }
        public decimal importe { get; set; }


        public RubroPatrimonioNetoDto() { }


        //public RubroPatrimonioNetoDto(RubroPatrimonioNeto rubro)
        //{

        //    denominacion = rubro.Denominacion;
        //    importe = rubro.Importe;
        //    codigo = rubro.Codigo;

        //}

        //public static RubroPatrimonioNetoDto ConvertirDesdeRubroPatrimonioNeto(RubroPatrimonioNeto rubro)
        //{
        //    RubroPatrimonioNetoDto dto = new RubroPatrimonioNetoDto();

        //    dto.codigo = rubro.Codigo;
        //    dto.denominacion = rubro.Denominacion;
        //    dto.importe = rubro.Importe;

        //    return dto;
        //}

        //public override bool Equals(object obj)
        //{
        //    return obj is RubroPatrimonioNetoDto dto &&
        //           codigo == dto.codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(codigo);
        //}
    }
}
