using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Model
{
    public class EntidadModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? RazonSocial { get; set; }
        public string? NroCorrelativo { get; set; }
        public string? Cuit { get; set; }
        public string? TipoEntidad { get; set; }
        public string? Estado { get; set; }
    }
}
