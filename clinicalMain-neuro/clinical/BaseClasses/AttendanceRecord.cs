using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class AttendanceRecord
    {
        public int RecordID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserID { get; set; }
        public bool IsPresent { get; set; }
        public AttendanceRecord(int recordID, DateTime timeStamp, int userID, bool isPresent)
        {
            RecordID = recordID;
            TimeStamp = timeStamp;
            UserID = userID;
            IsPresent = isPresent;
        }
    }
}
