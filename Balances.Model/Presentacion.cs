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
        /*public Presentacion(byte[] binariopdf)
        {
            this.binariopdf = binariopdf;
            this.Fecha = DateTime.Now;
        }*/
    }
}
