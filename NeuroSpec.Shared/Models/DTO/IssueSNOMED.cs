﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class IssueSNOMED
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int IssueID { get; set; }
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int VisitID { get; set; }
        public string Notes { get; set; }
        public string? SNOMEDID { get; set; }
        public string SNOMEDName { get; set; }

    }
}
