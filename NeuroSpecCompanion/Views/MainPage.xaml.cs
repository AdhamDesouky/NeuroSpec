using System;
using Microsoft.Maui.Controls;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;

namespace NeuroSpecCompanion.Views
{
    public partial class MainPage : ContentPage
    {
        AuthService _authService = new AuthService();
        public MainPage()
        {
            InitializeComponent();

        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            int username = int.Parse(userNameEntry.Text);

            string password = passwordEntry.Text;
            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Login Failed", "Invalid Login Credentials", "OK");
            }
            bool autoLogin = autoLoginSwitch.IsChecked;
            bool result = await _authService.VerifyPatientCallerAsync(username, password,autoLogin);

            if (result)
            {
                await DisplayAlert("Login Success", "Login Successful", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid Login Credentials", "OK");
            }
        }

        

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPages.RegisterPage());
        }
    }
}
