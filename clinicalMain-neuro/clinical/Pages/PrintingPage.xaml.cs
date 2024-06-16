using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace clinical.Pages
{

    public partial class PrintingPage : Page
    {
        
        IssueService issueService = new IssueService();
        public PrintingPage(Prescription plan)
        {
            InitializeComponent();
            List<Issue> Issues = issueService.GetAllIssuesByPrescriptionIDAsync(plan.PrescriptionID).Result;
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


