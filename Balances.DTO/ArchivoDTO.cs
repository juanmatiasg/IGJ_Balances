﻿namespace Balances.DTO
{
    public class ArchivoDTO
    {

        public string SesionId { get; set; }
        public string Id { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Hash { get; set; }
        public string NombreArchivo { get; set; }
        public long Tamaño { get; set; }
        public string ContentType { get; set; }


    }
}
