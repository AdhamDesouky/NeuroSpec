using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NeuroSpecCompanion.ViewModels
{
    public class ChatHomePageViewModel : ViewModelBase
    {
        //ObservableCollection<User> _users;
        //ObservableCollection<Message> _recentChat;

        public ChatHomePageViewModel()
        {
            //LoadData();
        }

        //public ObservableCollection<User> Users
        //{
        //    get { return _users; }
        //    set
        //    {
        //        _users = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ObservableCollection<Message> RecentChat
        //{
        //    get { return _recentChat; }
        //    set
        //    {
        //        _recentChat = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand DetailCommand => new Command<object>(OnNavigate);

        //void LoadData()
        //{
        //    Users = new ObservableCollection<User>(MessageService.Instance.GetUsers());
        //    RecentChat = new ObservableCollection<Message>(MessageService.Instance.GetChats());
        //}
    }
}

