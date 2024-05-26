using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Models.DTO
{
    public class BillingRecord
    {
        public int BillingID { get; set; }
        public double BillingAmount { get; set; }
        public int TreatmentPlanID { get; set; }
        public int PatientID { get; set; }

        public BillingRecord(int billingID, double billingAmount, int treatmentPlanID, int patientID)
        {
            BillingID = billingID;
            BillingAmount = billingAmount;
            TreatmentPlanID = treatmentPlanID;
            PatientID = patientID;
        }
    }
}
