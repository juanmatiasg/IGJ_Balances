﻿using Balances.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Balances.DTO
{
    public class CaratulaDto
    {


        [BsonElement("Email")]
        public string Email { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaDeCierre { get; set; }

        public Entidad Entidad { get; set; }
    }
}