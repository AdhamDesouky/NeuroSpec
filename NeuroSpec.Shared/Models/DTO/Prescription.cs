﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class Prescription
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int PrescriptionID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int VisitID { get; set; }

        
    }
}
