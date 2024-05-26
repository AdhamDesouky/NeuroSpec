using System.Collections.ObjectModel;
using System.Windows.Input;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Services;
using System.ComponentModel;


namespace NeuroSpecCompanion.ViewModels
{



    public class ChatBotMainViewModel:ViewModelBase
    {

        private readonly ChatbotService _chatBotService;
        private string _entryText;

        public ObservableCollection<HorizontalStackLayout> Messages { get; }
        public ICommand SendCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string EntryText
        {
            get => _entryText;
            set
            {
                _entryText = value;
                OnPropertyChanged(nameof(EntryText));
            }
        }

        public ChatBotMainViewModel()
        {
            _chatBotService = new ChatbotService();
            Messages = new ObservableCollection<HorizontalStackLayout>();
            SendCommand = new Command(Send);
        }

        private async void Send()
        {
            if (string.IsNullOrEmpty(EntryText))
            {
                return;
            }

            Messages.Add(CreateNewSenderMessage(EntryText));
            string text = EntryText;
            EntryText = "";

            string finalText = new string(text.Where(c => arabicLetters.Contains(c)).ToArray());
            Messages.Add(CreateNewSenderMessage("afterCleaning: " + finalText));

            if (string.IsNullOrEmpty(finalText))
            {
                Messages.Add(CreateNewResponseMessage("I'm sorry, I can't understand your message.\n Please Make Sure to type your message in Arabic."));
                ScrollToBottom();
                return;
            }

            string responseMessage = await _chatBotService.ProcessMessageAsync(finalText);
            Messages.Add(CreateNewResponseMessage(responseMessage));
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            // Implement scrolling if needed, usually handled in the view.
        }

        private HorizontalStackLayout CreateNewSenderMessage(string text)
        {
            var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            var horizontalStackLayout = new HorizontalStackLayout
            {
                FlowDirection = FlowDirection.RightToLeft,
                Children =
                {
                    new Frame
                    {
                        CornerRadius = 20,
                        Padding = 0,
                        Margin = new Thickness(10),
                        MinimumHeightRequest = 40,
                        MaximumWidthRequest = screenWidth - 30,

                        Content = new Grid
                        {
                            BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                            MinimumWidthRequest = 10,
                            Children =
                            {
                                new Label
                                {
                                    Text = text,
                                    FontSize = 18,
                                    FlowDirection = FlowDirection.RightToLeft,
                                    Margin = new Thickness(15)
                                }
                            }
                        }
                    }
                }
            };
            return horizontalStackLayout;
        }

        private HorizontalStackLayout CreateNewResponseMessage(string text)
        {
            var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            var horizontalStackLayout = new HorizontalStackLayout
            {
                FlowDirection = FlowDirection.LeftToRight,
                Children =
                {
                    new Frame
                    {
                        CornerRadius = 20,
                        Padding = 0,
                        Margin = new Thickness(10),
                        MaximumWidthRequest = screenWidth - 30,
                        MinimumHeightRequest = 40,
                        Content = new Grid
                        {
                            BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                            MinimumWidthRequest = 10,
                            Children =
                            {
                                new Label
                                {
                                    Text = text,
                                    FontSize = 18,
                                    FlowDirection = FlowDirection.LeftToRight,
                                    Margin = new Thickness(15)
                                }
                            }
                        }
                    }
                }
            };
            return horizontalStackLayout;
        }
        char[] arabicLetters = {'ء',' ', 'ا', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي', 'ى', 'أ', 'آ', 'إ' };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override Task InitializeAsync(object navigationData)
        {

            return base.InitializeAsync(navigationData);
        }

    }
    public class MessageModel
    {
        public string Text { get; set; }
        public bool IsSender { get; set; }
    }
}
