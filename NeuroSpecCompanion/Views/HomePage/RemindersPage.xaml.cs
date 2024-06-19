using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;

namespace NeuroSpecCompanion.Views
{
    public partial class RemindersPage : ContentPage
    {
        public ObservableCollection<Reminder> Reminders { get; set; }
        IssueDrugService drugService;
        VisitService visitService;
        AppointmentTypeService appointmentTypeService;

        public RemindersPage()
        {
            InitializeComponent();
            Reminders = new ObservableCollection<Reminder>();
            BindingContext = this;
            drugService = new IssueDrugService();
            visitService = new VisitService();
            appointmentTypeService = new AppointmentTypeService();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Fetch issued drugs
                var issuedDrugs = await drugService.GetAllIssueDrugsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
                Debug.WriteLine($"Fetched {issuedDrugs.Count} issued drugs");
                foreach (var drug in issuedDrugs)
                {
                    // Assuming the frequency contains timing information
                    var times = ParseFrequencyToTime(drug.Frequency);
                    foreach (var time in times)
                    {
                        Reminders.Add(new Reminder
                        {
                            Title = $"Take {drug.Name}",
                            Category = "Medication",
                            Time = time
                        });
                        Debug.WriteLine($"Added reminder for {drug.Name} at {time}");
                    }
                }

                // Fetch future visits
                var futureVisits = await visitService.GetAllVisitsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
                futureVisits = futureVisits.FindAll(v => v.TimeStamp.Date > DateTime.Now.Date);
                Debug.WriteLine($"Fetched {futureVisits.Count} future visits");
                foreach (var visit in futureVisits)
                {
                    Reminders.Add(new Reminder
                    {
                        Title = $"{(await appointmentTypeService.GetAppointmentTypeByIDAsync( visit.AppointmentTypeID)).Name}",
                        Category = "Appointment",
                        Time = visit.TimeStamp.TimeOfDay
                    });
                    Debug.WriteLine($"Added reminder for Doctor Appointment at {visit.TimeStamp.TimeOfDay}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private List<TimeSpan> ParseFrequencyToTime(string frequency)
        {
            var times = new List<TimeSpan>();
            int num = int.Parse(frequency.Split(' ')[0]);
            for (int i = 0; i < num; i++)
            {
                times.Add(new TimeSpan(((24/num) + i)%24, 0, 0));
            }
            
            return times;
        }

        private void OnAddReminderClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ReminderTitle.Text) && ReminderCategory.SelectedItem != null)
            {
                Reminders.Add(new Reminder
                {
                    Title = ReminderTitle.Text,
                    Category = ReminderCategory.SelectedItem.ToString(),
                    Time = ReminderTime.Time
                });

                ReminderTitle.Text = string.Empty;
                ReminderCategory.SelectedItem = null;
                ReminderTime.Time = new TimeSpan(0, 0, 0);
            }
        }

        private void OnDeleteReminderClicked(object sender, EventArgs e)
        {
            var reminder = (sender as Button).CommandParameter as Reminder;
            if (reminder != null)
            {
                Reminders.Remove(reminder);
            }
        }
    }

    public class Reminder
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public TimeSpan Time { get; set; }
    }
}
