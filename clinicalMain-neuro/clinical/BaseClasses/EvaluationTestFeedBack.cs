using System;

namespace clinical.BaseClasses
{
    class EvaluationTestFeedBack
    {
        public int TestFeedBackID { get; set; }
        public int Severity { get; set; }
        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public int TestID { get; set; }
        public string Notes { get; set; }
        public DateTime TimeStamp { get; set; }
        public EvaluationTestFeedBack(int testFeedBackID, int severity, int visitID, int patientID, int testID, string notes, DateTime timeStamp)
        {
            TestFeedBackID = testFeedBackID;
            Severity = severity;
            VisitID = visitID;
            PatientID = patientID;
            TestID = testID;
            Notes = notes;
            TimeStamp = timeStamp;
        }
    }
}
