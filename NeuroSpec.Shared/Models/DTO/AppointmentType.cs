using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class AppointmentType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int TypeID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeInMinutes {  get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return $"{Name}, ${Cost}";
        }
    }
}
