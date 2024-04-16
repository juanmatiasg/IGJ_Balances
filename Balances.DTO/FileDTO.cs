using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class FileDTO {
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Hash { get; set; }
        public string NombreArchivo { get; set; }
        public long Tamaño { get; set; }
        public string ContentType { get; set; }
       // public byte[] DatosBinarios { get; set; }
        public object Id { get; set; }

        public FileDTO() { 
            this.FechaCreacion = DateTime.Now;
        }
    
    }
}
