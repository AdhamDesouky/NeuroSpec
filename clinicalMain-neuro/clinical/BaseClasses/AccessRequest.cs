using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class AccessRequest
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string RequestDetails { get; set; }
        public int RequestedPatientID { get; set; }
        public int RequestedRecordID { get; set; }
        public AccessRequest(int requestID, int userID, DateTime timeStamp, string requestDetails, int requestedPatientID, int requestedRecordID)
        {
            RequestID = requestID;
            UserID = userID;
            TimeStamp = timeStamp;
            RequestDetails = requestDetails;
            RequestedPatientID = requestedPatientID;
            RequestedRecordID = requestedRecordID;
        }
    }
}
