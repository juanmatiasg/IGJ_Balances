using Balances.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Balances.DTO
{
    public class CaratulaDto
    {

        public string SesionId { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaDeCierre { get; set; }

        public DateTime Fecha { get; set; }

        public bool Rectificatorio { get; set; }
        public Entidad Entidad { get; set; }

        // Conversion method from Balance.Model.Caratula to CaratulaDto
        public static CaratulaDto FromCaratulaModel(Caratula caratulaModel)
        {
            return new CaratulaDto
            {
                Email = caratulaModel.Email,
                FechaInicio = caratulaModel.FechaInicio,
                FechaDeCierre = caratulaModel.FechaDeCierre,
                Fecha = caratulaModel.Fecha,
                Entidad = caratulaModel.Entidad
            };
        }
    }
}
