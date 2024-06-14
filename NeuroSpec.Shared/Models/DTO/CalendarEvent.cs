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
    public class CalendarEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int EventID {  get; set; }
        public int UserID {  get; set; }
        public string EventName { get; set; }
        public string EventText {  get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public bool IsDone {  get; set; }
    }
}
