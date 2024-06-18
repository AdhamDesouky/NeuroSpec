using NeuroSpecCompanion.Views.TapTest;
using NeuroSpecCompanion.Views.MemoryTest;
using NeuroSpecCompanion.Services;
using CommunityToolkit.Maui.Core.Views;
using NeuroSpecCompanion.Views.VibrationTest;
using NeuroSpecCompanion.Views.BookAppointment;
using NeuroSpecCompanion.Views;
using Microsoft.Maui.Controls;
using System.Threading;

namespace NeuroSpecCompanion.Views
{
    public partial class HomePage : ContentPage
    {
        private Timer _longPressTimer;

        public HomePage()
        {
            InitializeComponent();
            hellolbl.Text = $"Hello, {LoggedInPatientService.LoggedInPatient.FirstName}";

            // Initialize the timer
            _longPressTimer = new Timer(OnLongPressCompleted, null, Timeout.Infinite, Timeout.Infinite);

            // Add PanGestureRecognizer to the emergency button
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnEmergencyButtonPanUpdated;
            EmergencyButton.GestureRecognizers.Add(panGesture);
        }

        private void OnEmergencyButtonPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
            {
                _longPressTimer.Change(3000, Timeout.Infinite); // Start the timer for 3 seconds
            }
            else if (e.StatusType == GestureStatus.Canceled || e.StatusType == GestureStatus.Completed)
            {
                _longPressTimer.Change(Timeout.Infinite, Timeout.Infinite); // Stop the timer
            }
        }

        private void OnLongPressCompleted(object state)
        {
            // Run on the main thread
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var action = await DisplayActionSheet("Emergency Options", "Cancel", null, "Call Favorite Number", "Send Location via SMS");
                switch (action)
                {
                    case "Call Favorite Number":
                        MakeEmergencyCall();
                        break;
                    case "Send Location via SMS":
                        SendLocationViaSms();
                        break;
                }
            });
        }
        private async void OnHelpClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Emergency Options", "Cancel", null, "Call Emergency Contact", "Share Location via SMS");

            switch (action)
            {
                case "Call Emergency Contact":
                    await MakeEmergencyCall();
                    break;
                case "Share Location via SMS":
                    await SendLocationViaSms();
                    break;
                default:
                    // Cancel or other actions
                    break;
            }
        }
        private async Task MakeEmergencyCall()
        {
            try
            {
                PhoneDialer.Open("911"); // Replace with your emergency contact number
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                await DisplayAlert("Error", "Phone dialing is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                await DisplayAlert("Error", "Failed to make the emergency call.", "OK");
            }
        }

        private async Task SendLocationViaSms()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    var message = new SmsMessage("I need help. My current location is: " +
                                                 $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", new[] { "recipient_number" });
                    await Sms.ComposeAsync(message);
                }
                else
                {
                    await DisplayAlert("Error", "Failed to retrieve current location.", "OK");
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                // SMS is not supported on this device.
                await DisplayAlert("Error", "SMS is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                await DisplayAlert("Error", "Failed to send SMS.", "OK");
            }
        }

        // Other existing methods...

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
