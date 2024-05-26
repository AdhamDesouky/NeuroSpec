namespace NeuroSpec.Shared.Models.DTO
{
    public class ScanTest
    {
        public int ScanTestID { get; set; }
        public string ScanTestName { get; set; }
        public string RecommendedLab { get; set; }
        public string Notes { get; set; }
        public ScanTest(int scanTestID, string scanTestName, string recommendedLab, string notes)
        {
            ScanTestID = scanTestID;
            ScanTestName = scanTestName;
            RecommendedLab = recommendedLab;
            Notes = notes;
        }
        override public string ToString()
        {
            return ScanTestName;
        }
    }
}
