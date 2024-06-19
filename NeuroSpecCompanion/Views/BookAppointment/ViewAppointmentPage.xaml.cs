using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.ViewModels;
using Microsoft.Maui.Controls;

namespace NeuroSpecCompanion.Views.BookAppointment
{
    [QueryProperty(nameof(VisitJson), "Visit")]
    public partial class ViewAppointmentPage : ContentPage
    {
        private ViewAppointmentViewModel viewModel;

        public ViewAppointmentPage()
        {
            InitializeComponent();
            viewModel = new ViewAppointmentViewModel();
            BindingContext = viewModel;
        }

        private string visitJson;
        public string VisitJson
        {
            set
            {
                // Deserialize the Visit object from the query parameter
                if (value != null)
                {
                    visitJson = Uri.UnescapeDataString(value);
                    viewModel.Visit = Newtonsoft.Json.JsonConvert.DeserializeObject<Visit>(visitJson);
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
                viewModel.Visit = Newtonsoft.Json.JsonConvert.DeserializeObject<Visit>(visitJson);
            }
        }
    }
}
