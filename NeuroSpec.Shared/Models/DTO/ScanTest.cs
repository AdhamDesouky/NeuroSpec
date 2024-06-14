using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class ScanTest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int ScanTestID { get; set; }
        public string ScanTestName { get; set; }
        public string RecommendedLab { get; set; }
        public string Notes { get; set; }
        
        override public string ToString()
        {
            return ScanTestName;
        }
    }
}
