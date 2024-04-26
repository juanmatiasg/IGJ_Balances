namespace Balances.Model
{
    public class Archivo
    {
        public string Id { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Hash { get; set; }
        public string NombreArchivo { get; set; }
        public long Tamaño { get; set; }
        public string ContentType { get; set; }



        //public override bool Equals(object obj)
        //{
        //    return obj is Archivo archivo &&
        //           Id == archivo.Id &&
        //           Categoria == archivo.Categoria &&
        //           FechaCreacion == archivo.FechaCreacion &&
        //           Hash == archivo.Hash &&
        //           NombreArchivo == archivo.NombreArchivo &&
        //           Tamaño == archivo.Tamaño &&
        //           ContentType == archivo.ContentType;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Id, Categoria, FechaCreacion, Hash, NombreArchivo, Tamaño, ContentType);
        //}
    }
}
