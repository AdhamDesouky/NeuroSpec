using NeuroSpecCompanion.Views.TapTest;
using NeuroSpecCompanion.Views.MemoryTest;
using NeuroSpecCompanion.Services;
using CommunityToolkit.Maui.Core.Views;
namespace NeuroSpecCompanion.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        hellolbl.Text = $"Hello, {LoggedInPatientService.LoggedInPatient.FirstName}";

        //test
        DisplayAlert("Medical History", $"{LoggedInPatientService.LoggedInPatient.PatientID},{LoggedInPatientService.LoggedInPatient.LastName}", "OK");
        
	}

    private void RestingTremorClicked(object sender, TappedEventArgs e)
    {

    }

    private void MemoryTestClicked(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new MemoryGameTutorial());
    }

    private void VibrationClicked(object sender, TappedEventArgs e)
    {

    }

    private void TapTapTapClicked(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new TapGame());

    }

    private void HistorySquareClicked(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new MedicalHistoryPage());
    }

    private void signOut(object sender, EventArgs e)
    {
        LoggedInPatientService.LoggedInPatient = null;
        SecureStorage.SetAsync("PatientID", "");
        SecureStorage.SetAsync("Password", "");
        Shell.Current.GoToAsync("//MainPage");


    }
}