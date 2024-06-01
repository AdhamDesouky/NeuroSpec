using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpec.Shared.Globals
{
    public static class IDGeneration
    {
        public static int generateNewPatientID(string phoneNumber)
        {
            DateTime dateTime = DateTime.Now;
            string s = dateTime.DayOfYear.ToString();
            s += dateTime.Minute.ToString();
            s += phoneNumber.Substring(3, 3);
            return int.Parse(s);
        }
        public static DateTime CalculateBirthdate(int age)
        {
            DateTime currentDate = DateTime.Now;
            int birthYear = currentDate.Year - age;
            DateTime birthdate = new DateTime(birthYear, currentDate.Month, currentDate.Day);
            return birthdate;
        }

        public static int generateNewRecordID(int patientID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += patientID.ToString().Substring(0, 4);
            s += dateTime.DayOfYear.ToString();
            return int.Parse(s);
        }

        public static int generateNewAttendanceRecordID(int userID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += userID.ToString().Substring(0, 2);
            s += dateTime.DayOfYear.ToString();
            s += dateTime.Year.ToString().Substring(2, 2);
            return int.Parse(s);
        }

        public static int generateNewCalendarEventID(int userID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += userID.ToString().Substring(0, 2);
            s += dateTime.DayOfYear.ToString();
            s += dateTime.Year.ToString().Substring(2, 2);
            return int.Parse(s);
        }

        public static int generateNewDoctorID(string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "2";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);
        }

        public static int generateNewEmployeeID(string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "3";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);
        }

        public static string generateNewAdminID()
        {
            string s = "1";
            Random rand = new Random();
            s += rand.Next(100).ToString();
            return s;
        }

        public static int generateNewVisitID(int patID, DateTime time)
        {
            string s = patID.ToString().Substring(0, 3) + time.DayOfYear + time.Hour;
            return Convert.ToInt32(s);
        }

        public static int generateNewPaymentID(int patID, DateTime time)
        {
            string s = patID.ToString().Substring(2, 2) + new Random().Next(99).ToString() + time.DayOfYear.ToString() + time.Hour.ToString();
            return Convert.ToInt32(s);
        }

        public static int generateNewPrescriptionID(int visitID, DateTime time)
        {
            string s = visitID.ToString().Substring(0, 2) + time.Day + time.Minute + time.Second;
            return Convert.ToInt32(s);
        }

        public static int generateNewIssueExerciseID(int prescriptionID, int patientID)
        {
            string s = prescriptionID.ToString().Substring(0, 2) + new Random().Next(99).ToString() + patientID.ToString().Substring(0, 2) + new Random().Next(99).ToString();
            return Convert.ToInt32(s);
        }

        public static int generateNewTestFeedBackID(int visitID, int patientID)
        {
            string s = visitID.ToString().Substring(0, 2) + new Random().Next(99).ToString() + patientID.ToString().Substring(0, 2) + new Random().Next(99).ToString();
            return Convert.ToInt32(s);
        }

        public static int generateNewExerciseID()
        {
            string s = new Random().Next(99).ToString() + new Random().Next(99).ToString() + new Random().Next(81).ToString();
            return Convert.ToInt32(s);
        }

        public static int generateNewChatRoomID(int fID, int sID)
        {
            string s = fID.ToString().Substring(0, 3) + sID.ToString().Substring(0, 3);
            return Convert.ToInt32(s);
        }

        public static int generateNewChatMessageID(int senderID)
        {
            string s = senderID.ToString().Substring(0, 1) + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return Convert.ToInt32(s);
        }

    }
}
