using MvvmHelpers;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroSpecCompanion.ViewModels
{
    public class BookAppointmentViewModel : BaseViewModel
    {
        private readonly BookAppointmentService _appointmentService;
        private readonly AppointmentTypeService _appointmentTypeService;
        private readonly VisitService _visitService;
        public ObservableCollection<AppointmentType> AppointmentTypes { get; }
        public ObservableCollection<string> AvailableTimes { get; }

        public AppointmentType SelectedAppointmentType { get; set; }
        public DateTime SelectedDate { get; set; }
        public string SelectedTime { get; set; }
        public string Reason { get; set; }
        public bool IsUrgent { get; set; }

        public ICommand BookAppointmentCommand { get; }
        public ICommand SelectedDateChangedCommand { get; }

        public BookAppointmentViewModel()
        {
            _appointmentService = new BookAppointmentService();
            _appointmentTypeService = new AppointmentTypeService();

            AppointmentTypes = new ObservableCollection<AppointmentType>();
            AvailableTimes = new ObservableCollection<string>();

            SelectedDate = DateTime.Today;

            BookAppointmentCommand = new Command(OnBookAppointment);
            SelectedDateChangedCommand = new Command<DateTime>(OnSelectedDateChanged);

            LoadAppointmentTypes();
        }

        private async void LoadAppointmentTypes()
        {
            var types = await _appointmentTypeService.GetAllAppointmentTypesAsync();
            foreach (var type in types)
            {
                AppointmentTypes.Add(type);
            }
        }

        private async void OnSelectedDateChanged(DateTime selectedDate)
        {
            int doctorId = 0;
            SelectedDate= selectedDate;
            
            List<Visit> patientVisits = await _visitService.GetAllVisitsByPatientIDAsync(LoggedInPatientService.LoggedInPatient.PatientID); //changed the GetVisitsByPatientIDAsync to GetAllVisitsByPatientIDAsync
            if (patientVisits.Count > 0)
            {
                doctorId = patientVisits[patientVisits.Count - 1].DoctorID;
            }
            else doctorId = 0;
            AvailableTimes.Clear();
            var times = await _visitService.GetAvailableTimeSlotsOnDayAsync(SelectedDate,doctorId);
            foreach (var time in times)
            {
                AvailableTimes.Add(time);
            }
        }   

        private async void OnBookAppointment()
        {
            if (SelectedAppointmentType == null || string.IsNullOrEmpty(SelectedTime))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select appointment type and time.", "OK");
                return;
            }

            var appointmentRequest = new BookAppointmentRequest
            {
                PatientID = LoggedInPatientService.LoggedInPatient.PatientID,
                DoctorID = 1, // Replace with actual DoctorID
                AppointmentTypeID = SelectedAppointmentType.TypeID,
                AppointmentTime = DateTime.Parse($"{SelectedDate:yyyy-MM-dd} {SelectedTime}"),
                Reason = Reason,
                IsUrgent = IsUrgent,
                IsConfirmed = false // Default value
            };

            await _appointmentService.InsertBookAppointmentRequestAsync(appointmentRequest);
            await Application.Current.MainPage.DisplayAlert("Success", "Appointment booked successfully, You'll be contacted from the clinic soon.", "OK");

        }
    }
}
