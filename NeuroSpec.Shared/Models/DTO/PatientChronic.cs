using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class PatientChronic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public string ChronicName { get; set; }
        public string ChronicDescription { get; set; }
        public int PatientID { get; set; }


    }
}
