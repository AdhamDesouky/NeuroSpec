using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Models.DTO
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public int VisitID { get; set; }

        public Prescription() { }
        public Prescription(int prescriptionID, DateTime timeStamp, int patientID, int DoctorID, int visitID)
        {
            PrescriptionID = prescriptionID;
            TimeStamp = timeStamp;
            PatientID = patientID;
            UserID = DoctorID;
            VisitID = visitID;
            
        }
        //override public string ToString()
        //{
        //    return "ID: " + PrescriptionID.ToString() + ", Patient: " + DB.GetPatientById(PatientID).FullName;
        //}
    }
}
