namespace Balances.Model
{
    public class Presentacion
    {
        public byte[] PdfBytes;

        public DateTime Fecha;
        private byte[] binariopdf;

        public Presentacion(byte[] binariopdf)
        {
            this.binariopdf = binariopdf;
            this.Fecha = DateTime.Now;
        }
    }
}
