using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ChatMessage
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ChatRoomID { get; set; }
        public string MessageContent { get; set; }
        public DateTime TimeStamp { get; set; }

        public ChatMessage(int messageID, int senderID, int chatRoomID, string messageContent, DateTime timeStamp)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatRoomID = chatRoomID;
            MessageContent = messageContent;
            TimeStamp = timeStamp;
        }
    }
}
