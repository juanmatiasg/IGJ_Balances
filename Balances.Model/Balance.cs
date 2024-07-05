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

        //public List<Archivo> Archivos { get; set; }
        public ICollection<Archivo> Archivos { get; set; }
        public List<Autoridad> Autoridades { get; set; }
        public EstadoContable EstadoContable { get; set; }
        public Libros Libros { get; set; }
        public Socios Socios { get; set; }
        public Contador Contador { get; set; }
        public Presentacion Presentacion { get; set; }
        public string HASH { get; set; }
    }
}
