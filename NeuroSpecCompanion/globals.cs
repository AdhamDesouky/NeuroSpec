using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpecCompanion
{
    public class globals
    {
        public static string FormatTimeMinSeconds(int min, int sec)
        {
            return (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec;
        }
    }
}
