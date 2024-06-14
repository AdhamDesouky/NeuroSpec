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
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int PaymentID { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int DoctorID {  get; set; }
        public int PatientID {  get; set; }
        

    }
}
