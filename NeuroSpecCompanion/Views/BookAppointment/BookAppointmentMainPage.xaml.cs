using NeuroSpecCompanion.ViewModels;

namespace NeuroSpecCompanion.Views.BookAppointment;

public partial class BookAppointmentMainPage : ContentPage
{
	public BookAppointmentMainPage()
	{
		InitializeComponent();
        BindingContext = new BookAppointmentViewModel();

    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (BindingContext is BookAppointmentViewModel viewModel)
        {
            viewModel.SelectedDateChangedCommand.Execute(e.NewDate);
        }
    }
}