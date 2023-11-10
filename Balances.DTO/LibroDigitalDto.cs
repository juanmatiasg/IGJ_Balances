using Balances.Model;

namespace Balances.DTO
{
    public class LibroDigitalDto
    {
        public string? tipoDocumento { get; set; }
        public string? nombre { get; set; }
        public string? denominacion { get; set; }
        public string? numeroRl { get; set; }
        public DateTime fechaUltimaRegistracion { get; set; }
        public string? folio { get; set; }
        public bool? noSabeNoContesta { get; set; }

        public LibroDigitalDto() { }

        public LibroDigitalDto(LibroDigital libro)
        {
            nombre = libro.Nombre;
            denominacion = libro.Denominacion;
            numeroRl = libro.NumeroRl;
            fechaUltimaRegistracion = libro.FechaUltimaRegistracion;
            folio = libro.Folio;
            tipoDocumento = libro.TipoDocumento;
            noSabeNoContesta = libro.NoSabeNoContesta;


        }

        public LibroDigital GetLibroDigital()
        {


            var libro = new LibroDigital
            {
                Nombre = nombre,
                Denominacion = denominacion,
                NumeroRl = numeroRl,
                FechaUltimaRegistracion = fechaUltimaRegistracion /*DateTimeHelper.ConvertFromSpanish(fechaUltimaRegistracion)*/,
                Folio = folio,
                TipoDocumento = tipoDocumento,
                NoSabeNoContesta = (bool)noSabeNoContesta


            };

            return libro;



        }
    }
}
