namespace Dominio.Helpers
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
        public List<ArchivoAdjunto> Adjuntos { get; set; }
        public List<ImagenIncrustada> Imagenes { get; set; }


        public class ArchivoAdjunto
        {
            public string Nombre { get; set; }
            public string TipoArchivo { get; set; }
            public byte[] Binario { get; set; }
        }
        public class ImagenIncrustada
        {
            public string Nombre { get; set; }
            public string TipoArchivo { get; set; }
            public byte[] Binario { get; set; }
        }
        public MailRequest()
        {
            Adjuntos = new List<ArchivoAdjunto>();

        }
    }
}
