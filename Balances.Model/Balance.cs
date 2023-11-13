using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace Balances.Model
{
    public class Balance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember]
        [BsonElement("Caratula")]
        public Caratula Caratula { get; set; }

        public ICollection<Archivo> Archivos { get; set; }
        public RepresentanteLegales RepresentantesLegales { get; set; }
        public EstadoContable EstadoContable { get; set; }
        public List<LibroDigital> LibrosDigitales { get; set; }
        public Contador Contador { get; set; }
    }
}
