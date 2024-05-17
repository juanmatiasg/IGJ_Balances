namespace Balances.DTO
{
    public class RubrosPatrimonioNetoDto
    {
        /*public RubrosPatrimonioNetoDto() { }

        public RubrosPatrimonioNetoDto(List<RubroPatrimonioNeto> rubros)
        {

            if (rubros != null && rubros.Count > 0)
            {

                List<RubroPatrimonioNetoDto> otrosRrubros = new();

                rubros.ForEach(x => { otrosRrubros.Add(new RubroPatrimonioNetoDto(x)); });
            }
        }

        public List<RubroPatrimonioNeto> GetRubrosPatrimonioNeto()
        {
            List<RubroPatrimonioNeto> rubros = new();
            if (otrosRubros == null)
                otrosRubros = new();

            otrosRubros.ForEach(x => { rubros.Add(x.GetRubroPatrimonioNeto()); });

            return rubros;

        }*/
        public string? BalanceId { get; set; }
        public List<RubroPatrimonioNetoDto> otrosRubros { get; set; }

    }
}
