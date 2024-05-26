using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            // Navigate to the login page on app startup
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}
