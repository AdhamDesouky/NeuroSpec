using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class EvaluationTest
    {
        public int TestID { get; set; }
        public string TestName {  get; set; }
        public string TestDescription {  get; set; }

        public EvaluationTest(int testID, string testName, string testDescription)
        {
            TestID = testID;
            TestName = testName;
            TestDescription = testDescription;
        }

        public override string ToString()
        {
            return TestName;
        }
    }
}
