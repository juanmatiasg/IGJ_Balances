using Balances.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace Balances.DTO
{
    public class BalanceDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember]
        [BsonElement("Caratula")]
        public Caratula Caratula { get; set; }
        public List<ArchivoDTO> Archivos { get; set; }
        //public List<FileDTO> Archivos { get; set; }
        public List<AutoridadDto> Autoridades { get; set; }
        public EstadoContable EstadoContable { get; set; }
        public LibrosDto Libros { get; set; }
        public Contador Contador { get; set; }
        public SociosDto Socios { get; set; }
        public Presentacion Presentacion { get; set; }
        public string HASH { get; set; }

        // Constructor para asegurar la inicialización de Presentacion
        public BalanceDto()
        {

            Presentacion = new Presentacion();
        }
    }
}
