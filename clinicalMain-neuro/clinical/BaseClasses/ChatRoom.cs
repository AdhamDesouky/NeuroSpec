using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ChatRoom
    {
        public int ChatRoomID { get; set; }
        public int FirstUserID { get; set; }
        public int SecondUserID { get; set; }
        public string ChatRoomName { get; set; }
        public DateTime LastVisit { get; set; }
        public ChatMessage LastMessage { get { return DB.GetLastSentChatMessageByChatRoomID(ChatRoomID); } }

        // Constructor
        public ChatRoom(int chatRoomID, int firstUserID, int secondUserID, string chatRoomName, DateTime lastVisited)
        {
            ChatRoomID = chatRoomID;
            FirstUserID = firstUserID;
            SecondUserID = secondUserID;
            ChatRoomName = chatRoomName;
            LastVisit = lastVisited;
        }
    }
}
