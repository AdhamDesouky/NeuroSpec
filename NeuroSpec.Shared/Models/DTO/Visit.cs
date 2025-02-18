﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class Visit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int VisitID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentTypeID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TherapistNotes { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool IsDone { get; set; }

       
    }
}
