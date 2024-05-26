using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Models.DTO
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int DoctorID {  get; set; }
        public int PatientID {  get; set; }
        public Payment(int paymentID, double amount, DateTime timeStamp, int DoctorID, int patientID)
        {
            PaymentID = paymentID;
            Amount = amount;
            TimeStamp = timeStamp;
            DoctorID = DoctorID;
            PatientID = patientID;
        }

    }
}
