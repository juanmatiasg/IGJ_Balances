namespace Balances.Model
{
    public class Presentacion
    {

        public DateTime Fecha { get; set; }
        public byte[] BinarioPdf { get; set; }

        public Presentacion()
        {
            Fecha = DateTime.Now;
        }

    }
}
