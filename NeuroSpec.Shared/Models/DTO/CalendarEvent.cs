using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Models.DTO
{
    public class CalendarEvent
    {
        public int EventID {  get; set; }
        public int UserID {  get; set; }
        public string EventName { get; set; }
        public string EventText {  get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public bool IsDone {  get; set; }
        public CalendarEvent(int eventId,int userId,string eventName,string eventText, DateTime start, DateTime end,bool done)
        {
            EventID = eventId;
            UserID = userId;
            EventName = eventName;
            EventText = eventText;
            EventStartTime = start;
            EventEndTime = end;
            IsDone = done;
        }
    }
}
