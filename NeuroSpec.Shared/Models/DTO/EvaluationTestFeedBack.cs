﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class EvaluationTestFeedBack
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int TestFeedBackID { get; set; }
        public int Severity { get; set; }
        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public string TestName { get; set; }
        public string Notes { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }
}
