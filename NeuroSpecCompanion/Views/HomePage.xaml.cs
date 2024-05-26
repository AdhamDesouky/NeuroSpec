using NeuroSpecCompanion.Views.TapTest;
using NeuroSpecCompanion.Views.MemoryTest;
namespace NeuroSpecCompanion.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void RestingTremorClicked(object sender, TappedEventArgs e)
    {

    }

    private void MemoryTestClicked(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new MemoryGame());
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
}