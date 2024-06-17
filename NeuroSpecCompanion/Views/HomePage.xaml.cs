using NeuroSpecCompanion.Views.TapTest;
using NeuroSpecCompanion.Views.MemoryTest;
using NeuroSpecCompanion.Services;
using CommunityToolkit.Maui.Core.Views;
using NeuroSpecCompanion.Views.VibrationTest;
using NeuroSpecCompanion.Views.BookAppointment;
using NeuroSpecCompanion.Views;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            hellolbl.Text = $"Hello, {LoggedInPatientService.LoggedInPatient.FirstName}";
        }

        private void RestingTremorClicked(object sender, TappedEventArgs e)
        {
            // Add the implementation
        }

        private void MemoryTestClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new MemoryGameTutorial());
        }

        private void PathGameClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new PathGame());
        }

        private void VibrationClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new VibrationTestGamePage());
        }


        private void TapTapTapClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new TapGame());
        }

        private void HistorySquareClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new ViewAllAppointmentsPage());
        }

        private void signOut(object sender, EventArgs e)
        {
            LoggedInPatientService.LoggedInPatient = null;
            SecureStorage.SetAsync("PatientID", "");
            SecureStorage.SetAsync("Password", "");
            Shell.Current.GoToAsync("//MainPage");
        }

        private async void OnMedicsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PillsPage());
        }

        private async void OnReminderClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RemindersPage());
        }

        private async void OnContactDoctorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactDoctorPage());
        }
    }
}
