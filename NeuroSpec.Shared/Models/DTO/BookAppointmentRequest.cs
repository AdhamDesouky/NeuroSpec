using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class BookAppointmentRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int BookAppointmentRequestID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentTypeID { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Reason { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsConfirmed { get; set; }
        
    }
}
