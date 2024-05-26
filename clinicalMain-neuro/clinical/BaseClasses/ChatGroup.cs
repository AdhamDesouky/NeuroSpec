using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ChatGroup
    {
        public int ChatGroupID { get; set; }
        public List<int> UsersIDs { get; set; }
        public string ChatGroupName { get; set; }

        public ChatGroup(int chatGroupID, List<int> users, string chatGroupName)
        {
            ChatGroupID = chatGroupID;
            UsersIDs = users;
            ChatGroupName = chatGroupName;
        }
    }
}
