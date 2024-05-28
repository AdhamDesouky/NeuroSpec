namespace NeuroSpecCompanion.Views.MemoryTest;

public partial class MemoryGameTutorial : ContentPage
{
	public MemoryGameTutorial()
	{
		InitializeComponent();
	}

    private void StartGame(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MemoryGame());
    }
}