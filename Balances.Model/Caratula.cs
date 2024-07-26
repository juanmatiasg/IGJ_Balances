using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Balances.Model
{
    public class Caratula
    {


        [BsonElement("Email")]
        public string Email { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaDeCierre { get; set; }

        public DateTime Fecha { get; set; }

        public Entidad Entidad { get; set; }

        public bool Rectificatorio { get; set; }

        public Caratula()
        {

            this.Fecha = DateTime.Now;
        }
    }
}
