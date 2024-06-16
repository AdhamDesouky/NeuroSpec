using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion.Views
{
    public partial class RemindersPage : ContentPage
    {
        public ObservableCollection<Reminder> Reminders { get; set; }

        public RemindersPage()
        {
            InitializeComponent();
            Reminders = new ObservableCollection<Reminder>
            {
                new Reminder { Title = "Take Aspirin", Category = "Medication", Time = new TimeSpan(8, 0, 0) },
                new Reminder { Title = "Doctor Appointment", Category = "Appointment", Time = new TimeSpan(10, 0, 0) },
                new Reminder { Title = "Exercise", Category = "Other", Time = new TimeSpan(18, 0, 0) }
            };

            BindingContext = this;
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
