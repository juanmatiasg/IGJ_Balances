namespace Balances.DTO
{
    public class LibroDto
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Denominacion { get; set; }
        public string NumeroRubrica { get; set; }
        public DateTime FechaUltimaRegistracion { get; set; }
        public string Folio { get; set; }
        public bool NoSabeNoContesta { get; set; }

        //public LibroDigitalDto() { }

        //public LibroDigitalDto(LibroDigital libro)
        //{
        //    nombre = libro.Nombre;
        //    denominacion = libro.Denominacion;
        //    numeroRl = libro.NumeroRl;
        //    fechaUltimaRegistracion = libro.FechaUltimaRegistracion;
        //    folio = libro.Folio;
        //    tipoDocumento = libro.TipoDocumento;
        //    noSabeNoContesta = libro.NoSabeNoContesta;


        //}

        //public LibroDigital GetLibroDigital()
        //{


        //    var libro = new LibroDigital
        //    {
        //        Nombre = nombre,
        //        Denominacion = denominacion,
        //        NumeroRl = numeroRl,
        //        FechaUltimaRegistracion = fechaUltimaRegistracion /*DateTimeHelper.ConvertFromSpanish(fechaUltimaRegistracion)*/,
        //        Folio = folio,
        //        TipoDocumento = tipoDocumento,
        //        NoSabeNoContesta = (bool)noSabeNoContesta


        //    };

        //    return libro;



        //}
    }
}
