using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
namespace NeuroSpecCompanion.Views
{
    public partial class ContactDoctorPage : ContentPage
    {
        public ContactDoctorPage()
        {
            InitializeComponent();
        }

        private async void OnCallDoctorClicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("01115008374"); // Replace with the doctor's phone number
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to make a call.", "OK");
            }
        }

        private async void OnEmailDoctorClicked(object sender, EventArgs e)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Contacting my Doctor",
                    Body = "Dear Doctor,",
                    To = new List<string> { "a.ahmed2131@nu.edu.eg" } // Replace with the doctor's email
                };
                await Email.ComposeAsync(message);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to send email.", "OK");
            }
        }

        private async void OnVideoCallDoctorClicked(object sender, EventArgs e)
        {
            try
            {
                // Navigate to a page or open a video call app
                await DisplayAlert("Video Call", "Starting a video call...", "OK");
                // Implement actual video call functionality
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to start video call.", "OK");
            }
        }

        private async void OnRequestAlternativeClicked(object sender, EventArgs e)
        {
            // Implement logic for requesting alternative prescription
            await DisplayAlert("Request Alternative Prescription", "Requesting alternative prescription...", "OK");
            // Add your logic to handle the request
        }

        private async void OnEmergencyCallClicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("123"); // Emergency number for ambulance (replace with your country's emergency number)
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to make emergency call.", "OK");
            }
        }
    }
}