﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SBP_Mongo.Models
{
    public class Vrsta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Naziv { get; set; } = null!;
    }
}
