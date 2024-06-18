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

        IssueDrugService issueService = new IssueDrugService();
        IssueSNOMEDService issueSNOMEDService = new IssueSNOMEDService();
        Prescription Prescription = new Prescription();
        public PrintingPage(Prescription plan)
        {
            InitializeComponent();
            Prescription = plan;
            initAsync();

        }

        async void initAsync()
        {
            List<IssueDrug> Issues = await issueService.GetAllIssueDrugsByPrescriptionIDAsync(Prescription.PrescriptionID);
            foreach (var i in Issues)
            {
                mainStackPanel.Children.Add(CreatePrescripedObject(i));
            }
            List<IssueSNOMED> IssuesSnom = await issueSNOMEDService.GetAllIssuesByPrescriptionIDAsync(Prescription.PrescriptionID);
            foreach (var i in IssuesSnom)
            {
                mainStackPanel.Children.Add(CreatePrescripedObject(i));
            }
        }
        public TextBlock CreatePrescripedObject(IssueDrug issue)
        {
            TextBlock prescripedTextBlock = new TextBlock
            {
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = $"-{issue.Name}, {issue.Notes}",

                Margin = new Thickness(5)
            };
            return prescripedTextBlock;
        }
        public TextBlock CreatePrescripedObject(IssueSNOMED issue)
        {
            TextBlock prescripedTextBlock = new TextBlock
            {
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Text = $"-{issue.SNOMEDName}, {issue.Notes}",

                Margin = new Thickness(5)
            };
            return prescripedTextBlock;
        }


    }
}


