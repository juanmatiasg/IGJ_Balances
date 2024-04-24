namespace Balances.Model
{
    public class RegistroSesion
    {
        public string Id { get; set; }
        public DateTime FechaInicio { get; set; }
        private string _BalanceId;

        public string BalanceId
        {
            get
            {
                this.UltimoAcceso = DateTime.Now;
                return _BalanceId;
            }
            set { _BalanceId = value; }
        }

        public DateTime UltimoAcceso { get; set; }

        public RegistroSesion()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Id = "150";
            this.FechaInicio = DateTime.Now;
        }

        public void RegistrarBalance(string balanceId)
        {
            this.BalanceId = balanceId;
            this.UltimoAcceso = DateTime.Now;
        }
    }
}
