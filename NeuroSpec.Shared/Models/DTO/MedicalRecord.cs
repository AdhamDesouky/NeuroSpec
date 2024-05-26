using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Models.DTO
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Report { get; set; }
        public List<string> Images { get; set; }
        public int PatientID { get; set; }
        //public string PatientName { get
        //    {
        //        return DB.GetPatientById(PatientID).FullName;
        //    } }
        public string DoctorNotes { get; set; }

        // Constructor
        public MedicalRecord(int recordID, string type, DateTime timeStamp, string report, List<string> images, int patientID, string DoctorNotes)
        {
            RecordID = recordID;
            Type = type;
            TimeStamp = timeStamp;
            Report = report;
            Images = images;
            PatientID = patientID;
            DoctorNotes = DoctorNotes;
        }

    }
}
