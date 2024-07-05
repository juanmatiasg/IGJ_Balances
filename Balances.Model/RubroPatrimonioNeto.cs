namespace Balances.Model
{
    public class RubroPatrimonioNeto
    {

        public string Codigo { get; set; }
        public string Denominacion { get; set; }
        public decimal Importe { get; set; }



        public RubroPatrimonioNeto() { }

        public override bool Equals(object obj)
        {
            return obj is RubroPatrimonioNeto neto &&
                   Codigo == neto.Codigo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Codigo);
        }
    }


}
