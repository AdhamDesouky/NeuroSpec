using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace clinical.Pages
{

    public partial class PrintingPage : Page
    {
        
        IssueService issueService = new IssueService();
        Prescription Prescription= new Prescription();
        public PrintingPage(Prescription plan)
        {
            InitializeComponent();
           Prescription = plan;
            initAsync();

        }

        async void initAsync()
        {
            List<Issue> Issues = await issueService.GetAllIssuesByPrescriptionIDAsync(Prescription.PrescriptionID);
            foreach (var i in Issues)
            {
                mainStackPanel.Children.Add(CreatePrescripedObject(i));
            }

        }
        public TextBlock CreatePrescripedObject(Issue issue)
        {
            TextBlock prescripedTextBlock = new TextBlock
            {
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = $"-{issue.IssueID}, {issue.Notes}",

                Margin = new Thickness(5)
            };
            return prescripedTextBlock;
        }



    }
}


