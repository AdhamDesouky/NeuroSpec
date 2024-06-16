using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion.Views
{
    public partial class PillsPage : ContentPage
    {
        public ObservableCollection<Assessment> Assessments { get; set; }

        public PillsPage()
        {
            InitializeComponent();
            Assessments = new ObservableCollection<Assessment>
            {
                new Assessment
                {
                    AssessmentDate = "June 15, 2023",
                    Medications = new ObservableCollection<Medication>
                    {
                        new Medication { Name = "Aspirin", Frequency = "100mg, once daily" },
                        new Medication { Name = "Vitamin D", Frequency = "2000 IU, once daily" }
                    }
                },
                new Assessment
                {
                    AssessmentDate = "July 20, 2023",
                    Medications = new ObservableCollection<Medication>
                    {
                        new Medication { Name = "Metformin", Frequency = "500mg, twice daily" }
                    }
                },
                new Assessment
                {
                    AssessmentDate = "August 18, 2023",
                    Medications = new ObservableCollection<Medication>
                    {
                        new Medication { Name = "Lisinopril", Frequency = "10mg, once daily" }
                    }
                },
                new Assessment
                {
                    AssessmentDate = "September 22, 2023",
                    Medications = new ObservableCollection<Medication>
                    {
                        new Medication { Name = "Atorvastatin", Frequency = "20mg, once daily" }
                    }
                },
                new Assessment
                {
                    AssessmentDate = "October 10, 2023",
                    Medications = new ObservableCollection<Medication>
                    {
                        new Medication { Name = "Amlodipine", Frequency = "5mg, once daily" }
                    }
                }
            };

            BindingContext = this;
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
