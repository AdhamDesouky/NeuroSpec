using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentTypeID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get { return DB.GetAppointmentTypeByID(AppointmentTypeID).Name; } }
        public string TherapistNotes { get; set; }
        public string PatientName { get { return DB.GetPatientById(PatientID).FullName;} }
        public string DoctorName { get {return DB.GetUserById(DoctorID).FullName;} }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool IsDone { get; set; }

        // Constructor
        public Visit(int visitID, int DoctorID, int patientID, DateTime timeStamp, string therapistNotes, double height, double weight, bool isDone, int appointmentTypeId)
        {
            this.VisitID = visitID;
            this.DoctorID = DoctorID;
            this.PatientID = patientID;
            this.TimeStamp = timeStamp;
            this.TherapistNotes = therapistNotes;
            this.Weight=weight; 
            this.Height=height;
            this.IsDone = isDone;
            this.AppointmentTypeID = appointmentTypeId;
        }
    }
}
