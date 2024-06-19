using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Views
{
    public partial class PillsPage : ContentPage
    {
        public ObservableCollection<Assessment> Assessments { get; set; }
        IssueDrugService drugService;
        PrescriptionService prescriptionService;

        public PillsPage()
        {
            InitializeComponent();
            Assessments = new ObservableCollection<Assessment>();
            BindingContext = this;
            drugService = new IssueDrugService();
            prescriptionService = new PrescriptionService();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var prescriptions = await prescriptionService.GetAllPrescriptionsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
                foreach (var prescription in prescriptions)
                {
                    var drugs = await drugService.GetAllIssueDrugsByPrescriptionIDAsync(prescription.PrescriptionID);
                    var assessment = new Assessment
                    {
                        AssessmentDate = prescription.TimeStamp.Date.ToShortDateString(),
                        Medications = new ObservableCollection<Medication>()
                    };
                    foreach (var drug in drugs)
                    {
                        assessment.Medications.Add(new Medication
                        {
                            Name = drug.Name,
                            Frequency = drug.Frequency
                        });
                    }
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Assessments.Add(assessment);
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class Assessment
    {
        public string AssessmentDate { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
    }

    public class Medication
    {
        public string Name { get; set; }
        public string Frequency { get; set; }
    }
}
