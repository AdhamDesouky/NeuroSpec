using clinical.BaseClasses;
using clinical.userControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    
    public partial class ChatPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        User loggedIn = null;
        ChatRoom selectedChatRoom=null;
        public ChatPage(User user)
        {
            InitializeComponent();
            loggedIn = user;
            List<ChatRoom> chatRooms=DB.GetChatRoomsByUserID(user.UserID);
            
            foreach (ChatRoom chatRoom in chatRooms)
            {
                chatItem item = new chatItem();
                item.TagName = chatRoom.ChatRoomName.Substring(0,1);
                item.Title = chatRoom.ChatRoomName;
                if (chatRoom.LastMessage == null)
                {
                    item.Message = "No messages yet";
                }
                else
                {

                    item.Message = $"{DB.GetUserById(chatRoom.LastMessage.SenderID).FirstName}: {chatRoom.LastMessage.MessageContent}";
                }
                item.MouseDown += new MouseButtonEventHandler((s, e) => chatRoom_MouseDown(s, e, chatRoom));
                chatStack.Children.Add(item);
            }

            selectedChatRoom = DB.GetChatRoomByID(0);
            Refresh();
            textBoxMessage.Focus();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            inRefresh();
        }

        void Refresh()
        {
            roomName.Text = selectedChatRoom.ChatRoomName;
            texts.Children.Clear();
            foreach (ChatMessage ch in DB.GetAllChatMessagesByRoomID(selectedChatRoom.ChatRoomID))
            {
                //if my message -> 
                if (ch.SenderID == loggedIn.UserID)
                {
                    MyMessageChat ms = new MyMessageChat();
                    ms.Message = ch.MessageContent;
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.ToString("t");
                    myTime.Style = FindResource("timeTextRight") as Style;

                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);

                }
                else
                {
                    if (ch.ChatRoomID == 0)
                    {
                        UserChat user = new UserChat();
                        user.Username = DB.GetUserById(ch.SenderID).FullName;
                        texts.Children.Add(user);
                    }
                    MessageChat ms = new MessageChat();
                    ms.Message = ch.MessageContent;
                    ms.Color = (Brush)FindResource("darkerColor");
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.ToString("t");
                    myTime.Style = FindResource("timeText") as Style;

                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);
                }



            }
        }

        void inRefresh()
        {
            foreach (ChatMessage ch in DB.GetAllUnreadMessagesByRoomID(selectedChatRoom.ChatRoomID))
            {
                //if my message -> 
                if (ch.SenderID == loggedIn.UserID)
                {
                    MyMessageChat ms = new MyMessageChat();
                    ms.Message = ch.MessageContent;
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.ToString("t");
                    myTime.Style = FindResource("timeTextRight") as Style;

                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);

                }
                else
                {
                    if (ch.ChatRoomID == 0)
                    {
                        UserChat user = new UserChat();
                        user.Username = DB.GetUserById(ch.SenderID).FullName;
                        texts.Children.Add(user);
                    }
                    

                    MessageChat ms = new MessageChat();
                    ms.Message = ch.MessageContent;
                    ms.Color = (Brush)FindResource("darkerColor");
                    TextBlock myTime = new TextBlock();
                    myTime.Text = ch.TimeStamp.ToString("t");
                    myTime.Style = FindResource("timeText") as Style;
                    texts.Children.Add(ms);
                    texts.Children.Add(myTime);
                }



            }
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight);

            DB.UpdateLastVisitChatRoom(selectedChatRoom.ChatRoomID);

        }
        private void chatRoom_MouseDown(object sender, MouseButtonEventArgs e,ChatRoom chatRoom)
        {
            timer.Stop();
            if (selectedChatRoom!=null) DB.UpdateLastVisitChatRoom(selectedChatRoom.ChatRoomID);
            
            selectedChatRoom = chatRoom;
            Refresh();

            timer.Start();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            send();
        }
        void send()
        {
            string text = textBoxMessage.Text;
            int id = globals.generateNewChatMessageID(loggedIn.UserID);
            int id2 = globals.generateNewChatMessageID(selectedChatRoom.SecondUserID);
            int chatRoomID = selectedChatRoom.ChatRoomID;
           
            ChatMessage message = new ChatMessage(id, loggedIn.UserID, chatRoomID, text, DateTime.Now);

            DB.InsertChatMessage(message);
            textBoxMessage.Text = "";
        }

        private void enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { send(); }
        }

        private void annoncementsChannelClick(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            selectedChatRoom = DB.GetChatRoomByID(0);
                        
            Refresh();

            timer.Start();
        }
    }
}
