using System;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = userNameEntry.Text;
            string password = passwordEntry.Text;
            if (ValidateLogin(username, password))
            {
                await DisplayAlert("Login Success", "Login Successful", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid Login Credentials", "OK");
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Simulate a successful login for demonstration purposes
            return username == "adham" && password == "adham";
        }
    }
}
