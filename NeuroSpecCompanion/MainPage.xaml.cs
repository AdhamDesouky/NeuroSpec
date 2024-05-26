using System;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = userNameEntry.Text;
            string password = passwordEntry.Text;
            if(ValidateLogin(username, password))
            {
                DisplayAlert("Login Success", "Login Successful", "OK");
                Shell.Current.GoToAsync("//Home");
            }
            else
            {
                DisplayAlert("Login Failed", "Invalid Login Credentials", "OK");
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }
    }
}