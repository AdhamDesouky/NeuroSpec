using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.ViewModels;

namespace NeuroSpecCompanion.Views.BookAppointment;
[QueryProperty(nameof(Visit), "Visit")]
public partial class ViewAppointmentPage : ContentPage
{
    public Visit Visit { get; set; }

    public ViewAppointmentPage()
    {
        InitializeComponent();
        BindingContext = new ViewAppointmentViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ViewAppointmentViewModel viewModel)
        {
            viewModel.Visit = Visit;
        }
    }
}