using Balances.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class ListaDeLibrosDTO
    {

        private List<LibroDto> libros;

        public ListaDeLibrosDTO()
        {
            libros = new List<LibroDto>();
        }

        public void AgregarLibro(LibroDto libro)
        {
            libros.Add(libro);
        }

        public void EliminarLibro(LibroDto libro)
        {
            libros.Remove(libro);
        }

       

        public void AgregarLibrosDeLibrosDto(LibrosDto librosDto)
        {
            AgregarLibro(librosDto.Memoria);
            AgregarLibro(librosDto.Administracion);
            AgregarLibro(librosDto.Asamblea);
            AgregarLibro(librosDto.AsistenciaAsamblea);
            AgregarLibro(librosDto.SituacionPatrimonial);
            AgregarLibro(librosDto.Resultados);
            AgregarLibro(librosDto.PatrimonioNeto);
            AgregarLibro(librosDto.Efectivo);
            AgregarLibro(librosDto.Informacion);
            AgregarLibro(librosDto.EstadosContablesConsolidados);
            AgregarLibro(librosDto.Fiscalizacion);
            AgregarLibro(librosDto.Auditor);
            AgregarLibro(librosDto.IVA);
            AgregarLibro(librosDto.IVACompras);
            AgregarLibro(librosDto.IVAVentas);
            AgregarLibro(librosDto.LibroDiario);
        }

        public List<LibroDto> ObtenerLibros()
        {
            return libros;
        }

        public void Limpiar()
        {
            libros.Clear();
        }
    }
}
