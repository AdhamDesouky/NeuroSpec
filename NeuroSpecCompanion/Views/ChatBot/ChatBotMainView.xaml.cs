using CommunityToolkit.Maui.Core.Views;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text;
using NeuroSpecCompanion.Services;

namespace NeuroSpecCompanion.Views.ChatBot;

public partial class ChatBotMainView : ContentPage
{
	private readonly ChatbotService _chatBotService;
    public ChatBotMainView()
	{
		InitializeComponent();
        _chatBotService = new ChatbotService();
    }


    public HorizontalStackLayout CreateNewSenderMessage(string text) {
        var screenWidth = Window.Width;

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
                        MaximumWidthRequest = screenWidth-30,

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
    public HorizontalStackLayout CreateNewResponseMessage(string text)
    {
        var screenWidth = Window.Width;

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
                        MaximumWidthRequest = screenWidth-30,
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
    char[] arabicLetters = {'ء',' ', 'ا', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي','ى','أ', 'آ','إ' };

    private async void Send(object sender, EventArgs e)
    {
        string text = chatEntry.Text;
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        chatStack.Children.Add(CreateNewSenderMessage(text));
        chatEntry.Text = "";

        string finalText = "";
        foreach (char i in text)
        {
            if (arabicLetters.Contains(i))
            {
                finalText += i;
            }
        }
        //chatStack.Children.Add(CreateNewSenderMessage("afterCleaning: " + finalText));

        if (string.IsNullOrEmpty(finalText))
        {
            chatStack.Children.Add(CreateNewResponseMessage("I'm sorry, I can't understand your message.\n Please Make Sure to type your message in Arabic."));
            ScrollToBottom();
            return;
        }

        string responseMessage = await _chatBotService.ProcessMessageAsync(finalText);
        chatStack.Children.Add(CreateNewResponseMessage(responseMessage));
        ScrollToBottom();
    }

    private async void ScrollToBottom()
    {
        await Task.Delay(100);
        await chatScrollView.ScrollToAsync(0, chatStack.Height, true);
    }
}