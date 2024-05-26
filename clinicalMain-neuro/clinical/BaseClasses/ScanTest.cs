using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ScanTest
    {
        public int ScanTestID { get; set; }
        public string ScanTestName { get; set; }
        public string RecommendedLab { get; set; }
        public string Notes {  get; set; }
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
