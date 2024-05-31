using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;

namespace NeuroSpecCompanion
{
    public partial class App : Application
    {
        private readonly AuthService _authService=new AuthService();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            // Navigate to the login page on app startup
            Shell.Current.GoToAsync("//MainPage");
        }
        protected override async void OnStart()
        {
            base.OnStart();

            bool autoLoginSuccessful = await _authService.AutoLoginAsync();
            MainPage = new AppShell();

            if (autoLoginSuccessful)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
