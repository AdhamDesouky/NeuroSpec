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
    /// <summary>
    /// Refrences DiagonsticReport in FHIR
    /// </summary>
    public class MedicalRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int RecordID { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Report { get; set; }
        public List<Attachment> VisualAttachments { get; set; }
        public int PatientID { get; set; }
        public string DoctorNotes { get; set; }

        

    }
    public class Attachment
    {
        public string url { get; set; }
        public string title { get; set; }
        public string contentType { get; set; }

    }
}
