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
    }
}
