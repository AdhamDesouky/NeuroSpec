using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace clinical.Pages.reciptionistPages
{
    /// <summary>
    /// Interaction logic for reciptionistViewVisit.xaml
    /// </summary>
    public partial class reciptionistViewVisit : Page
    {
        Visit currVisit;
        PatientService patientService = new PatientService();
        Patient patient;
        User user;
        UserService userService = new UserService();
        AppointmentTypeService appointmentTypeService = new AppointmentTypeService();
        AppointmentType appointmentType;
        PrescriptionService prescriptionService = new PrescriptionService();
        VisitService visitService= new VisitService();
        public reciptionistViewVisit(Visit visit)
        {
            InitializeComponent();
            currVisit = visit;

            initAsync();
            mainTxt.Text = $"{patient.FirstName}, {appointmentType.Name}, {visit.TimeStamp}";
            idTextBox.Text = visit.VisitID.ToString();
            patientNameTextBox.Text = patient.FirstName;
            DoctorTextBox.Text = user.FirstName+" "+user.LastName;
            dpDatePicker.SelectedDate = visit.TimeStamp;
            typeTextBox.Text = appointmentType.Name;
            selectedDateTime = currVisit.TimeStamp;
            List<string> times = new List<string>();
            for (int i = 9; i <= 18; i++) //slots here
            {
                times.Add($"{i}:00");
                times.Add($"{i}:30");

            }
            timeCB.ItemsSource = times;
            timeCB.SelectedItem = visit.TimeStamp.ToString("HH:mm");
            


        }
        async void initAsync()
        {
            patient = await patientService.GetPatientByIdAsync(currVisit.PatientID);
            user = await userService.GetUserByIdAsync(currVisit.DoctorID);
            appointmentType = await appointmentTypeService.GetAppointmentTypeByIDAsync(currVisit.AppointmentTypeID);
            List<Prescription> visitPrescriptions = await prescriptionService.GetAllPrescriptionsByVisitIDAsync(currVisit.VisitID);
            foreach (var i in visitPrescriptions)
            {
                prescriptionsStackPanel.Children.Add(await globals.CreatePrescriptionUI(i));
            }
        }

        async void Refresh()
        {
            List<string> availTimes = await globals.GetAvailableTimeSlotsOnDayAsync(dpDatePicker.SelectedDate.Value, currVisit.DoctorID);
            if (availTimes.Count == 0)
            {
                dpDatePicker.SelectedDate.Value.AddDays(1);
            }
            timeCB.ItemsSource = availTimes;
        }
        private void viewDoctor(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new reciptionistViewDoctor(user));
        }

        private void viewPatient(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new reciptionistViewPatient(patient));

        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();

        }
        DateTime selectedDateTime;
        private void timeChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)timeCB.SelectedItem == null) return;
            string s = (string)timeCB.SelectedItem;
            string hr = "", min = "";
            hr += s[0];
            hr += s[1];
            min += s[3];
            min += s[4];

            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, int.Parse(hr), int.Parse(min), 0);

        }

        private void editVisitInfo(object sender, MouseButtonEventArgs e)
        {
            timeCB.IsEnabled = !timeCB.IsEnabled;
            dpDatePicker.IsEnabled = !dpDatePicker.IsEnabled;
            if (timeCB.IsEnabled)
            {
                Refresh();
            }
            else
            {
                string s = currVisit.TimeStamp.ToString("HH:mm");
                timeCB.SelectedItem = s;
                dpDatePicker.SelectedDate = currVisit.TimeStamp;

            }
        }

        private async void syncInfo(object sender, MouseButtonEventArgs e)
        {
            currVisit.TimeStamp = selectedDateTime;
            await visitService.UpdateVisitAsync(currVisit.VisitID, currVisit);
            timeCB.IsEnabled = !timeCB.IsEnabled;
            dpDatePicker.IsEnabled = !dpDatePicker.IsEnabled;

        }

        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDateTime = new DateTime(dpDatePicker.SelectedDate.Value.Year, dpDatePicker.SelectedDate.Value.Month, dpDatePicker.SelectedDate.Value.Day, selectedDateTime.Hour, selectedDateTime.Minute, 0);
        }

        private async void cancelVisit(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this visit?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;

            await visitService.DeleteVisitAsync(currVisit.VisitID);
            if (NavigationService.CanGoBack) NavigationService.GoBack();
        }

        private async void markVisitDoneOrUnDone(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to mark this visit as done?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;
            currVisit.IsDone = !currVisit.IsDone;
            await visitService.UpdateVisitAsync(currVisit.VisitID,currVisit);
            markDoneTB.Text = currVisit.IsDone ? "Mark as Undone" : "Mark as Done";
            MessageBox.Show("Visit Status Updated");


        }
    }
}
