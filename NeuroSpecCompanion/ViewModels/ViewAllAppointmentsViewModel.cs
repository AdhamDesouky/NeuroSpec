using MvvmHelpers;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Views;
using NeuroSpecCompanion.Views.BookAppointment;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NeuroSpecCompanion.ViewModels
{
    public class ViewAllAppointmentsViewModel : BaseViewModel
    {
        private readonly VisitService _visitService;
        public ObservableCollection<Visit> Visits { get; }

        public ICommand DeleteVisitCommand { get; }
        public ICommand ViewVisitCommand { get; }
        public ICommand BookAppointmentCommand { get; }

        public ViewAllAppointmentsViewModel()
        {
            _visitService = new VisitService();
            Visits = new ObservableCollection<Visit>();
            DeleteVisitCommand = new Command<Visit>(OnDeleteVisitClicked);
            ViewVisitCommand = new Command<Visit>(OnViewVisitClicked);
            BookAppointmentCommand = new Command(OnBookAppointmentClicked);

            LoadVisits();
        }

        private void OnBookAppointmentClicked()
        {
            Shell.Current.GoToAsync($"{nameof(BookAppointmentMainPage)}");
        }

        private async void LoadVisits()
        {
            var visits = await _visitService.GetVisitsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
            foreach (var visit in visits)
            {
                Visits.Add(visit);
            }
        }

        private async void OnDeleteVisitClicked(Visit visit)
        {
            try
            {
                await _visitService.DeleteVisitAsync(visit.VisitID);
                Visits.Remove(visit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async void OnViewVisitClicked(Visit visit)
        {
            await Shell.Current.GoToAsync($"{nameof(ViewAppointmentPage)}", new Dictionary<string, object>
            {
                { "Visit", visit }
            });
        }
    }
}
