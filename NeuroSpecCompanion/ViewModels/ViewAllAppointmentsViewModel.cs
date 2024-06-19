using MvvmHelpers;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
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
        private readonly BookAppointmentService _bookAppointmentService;
        public ObservableCollection<Visit> PastVisits { get; }
        public ObservableCollection<BookAppointmentRequest> NotYetConfirmedVisits { get; }
        public ObservableCollection<BookAppointmentRequest> UpcomingVisits { get; }

        public ICommand DeleteVisitCommand { get; }
        public ICommand ViewVisitCommand { get; }
        public ICommand BookAppointmentCommand { get; }

        public ViewAllAppointmentsViewModel()
        {
            _visitService = new VisitService();
            _bookAppointmentService = new BookAppointmentService();
            PastVisits = new ObservableCollection<Visit>();
            NotYetConfirmedVisits = new ObservableCollection<BookAppointmentRequest>();
            UpcomingVisits = new ObservableCollection<BookAppointmentRequest>();
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
            // Load past visits
            var visits = await _visitService.GetAllVisitsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
            foreach (var visit in visits)
            {
                PastVisits.Add(visit);
            }

            // Load appointment requests
            var appointments = await _bookAppointmentService.GetBookAppointmentRequestsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID);
            foreach (var appointment in appointments)
            {
                if (!appointment.IsConfirmed)
                {
                    NotYetConfirmedVisits.Add(appointment);
                }
                else if (appointment.AppointmentTime > DateTime.Now)
                {
                    UpcomingVisits.Add(appointment);
                }
            }
        }

        private async void OnDeleteVisitClicked(Visit visit)
        {
            try
            {
                await _visitService.DeleteVisitAsync(visit.VisitID);
                PastVisits.Remove(visit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async void OnViewVisitClicked(Visit visit)
        {
            string visitJson = Newtonsoft.Json.JsonConvert.SerializeObject(visit);
            await Shell.Current.GoToAsync($"{nameof(ViewAppointmentPage)}", new Dictionary<string, object>
            {
                { "Visit", visitJson }
            });
        }
    }
}
