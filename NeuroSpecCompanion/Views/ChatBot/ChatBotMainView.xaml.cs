using CommunityToolkit.Maui.Core.Views;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text;
using NeuroSpecCompanion.Services;
using NeuroSpec.Shared.Services.OntologyServices;
using NeuroSpec.Shared.Models.Ontology;

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
                            BackgroundColor = Color.FromHex("#324161"),
                            MinimumWidthRequest = 10,
                            Children =
                            {
                                new Label
                                {
                                    Text = text,
                                    FontSize = 18,
                                    FontAttributes= FontAttributes.Bold,
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
                            BackgroundColor = (Color)Application.Current.Resources["Primary"],
                            MinimumWidthRequest = 10,
                            Children =
                            {
                                new Label
                                {
                                    Text = text,
                                    Margin= new Thickness(15,15,30,15),
                                    FontSize = 18,
                                    FontAttributes= FontAttributes.Bold,
                                    FlowDirection = FlowDirection.LeftToRight,
                                }
                            }
                        }
                    }
                }
        };
        return horizontalStackLayout;
    }
    char[] arabicLetters = {'ء',' ', 'ا', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي','ى','أ', 'آ','إ' };


    ////this shouldnt be here, im testing
    //DrugOntologyService ontologyService = new DrugOntologyService();

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

        //for kamal and galal's model
        //string responseMessage=await _chatBotService.ProcessMessageAsync(finalText);

        //for generative AI model
        string responseMessage = await _chatBotService.ProcessMessageAsync(text);
        chatStack.Children.Add(CreateNewResponseMessage(responseMessage));

        //List<DrugOntology> searchResults = await ontologyService.GetDrugOntologyAsync(text);
        //string res="";
        //foreach (var item in searchResults)
        //{
        //    res += item.Name + "\n";
        //}
        //chatStack.Children.Add(CreateNewResponseMessage(res));
        ScrollToBottom();
    }

    private async void ScrollToBottom()
    {
        await Task.Delay(100);
        await chatScrollView.ScrollToAsync(0, chatStack.Height, true);
    }
}