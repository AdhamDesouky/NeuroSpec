namespace NeuroSpec.Shared.Models.DTO
{
    /// <summary>
    /// Could be a medical test, a lab test, a diagnostic test or a medical imaging, visual scan (MRI, X-Ray, CT).
    /// </summary>
    public class IssueTest : Issue
    {
        
        public int TestID { get; set; }
        public string TestName { get; set; }
        //public string TestResult { get; set; }
        public string TestUnit { get; set; }
        public string TestRange { get; set; }

    }
}
