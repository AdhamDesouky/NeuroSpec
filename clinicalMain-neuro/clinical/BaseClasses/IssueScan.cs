using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    /*
     
     CREATE TABLE IssueScan (
    issueID INT NOT NULL,
    scanTestID INT NOT NULL,
    prescriptionID INT NOT NULL,
    patientID INT NOT NULL,
    notes TEXT,
    PRIMARY KEY (issueID),
    FOREIGN KEY (scanTestID) REFERENCES scanTest(scanTestID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);
     */
    public class IssueScan
    {
        public int IssueID { get; set; }
        public int ScanTestID { get; set; }
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public string Notes { get; set; }

        public IssueScan(int issueID, int scanTestID, int prescriptionID, int patientID, string notes)
        {
            IssueID = issueID;
            ScanTestID = scanTestID;
            PrescriptionID = prescriptionID;
            PatientID = patientID;
            Notes = notes;
        }
    }
}
