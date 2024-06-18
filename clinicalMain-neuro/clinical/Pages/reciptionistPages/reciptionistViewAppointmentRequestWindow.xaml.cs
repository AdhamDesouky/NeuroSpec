using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace clinical.Pages.reciptionistPages
{
    /// <summary>
    /// Interaction logic for reciptionistViewAppointmentRequestWindow.xaml
    /// </summary>
    public partial class reciptionistViewAppointmentRequestWindow : Window
    {
        BookAppointmentRequest currRequest;

        PatientService patientService = new PatientService();
        Patient patient;
        User user;
        UserService userService = new UserService();
        AppointmentTypeService appointmentTypeService = new AppointmentTypeService();
        AppointmentType appointmentType;
        BookAppointmentService appointmentService = new BookAppointmentService();
        public reciptionistViewAppointmentRequestWindow(BookAppointmentRequest bookAppointmentRequest)
        {
            InitializeComponent();
            currRequest = bookAppointmentRequest;

            initAsync();


        }
        async void initAsync()
        {
            patient = await patientService.GetPatientByIdAsync(currRequest.PatientID);
            user = await userService.GetUserByIdAsync(currRequest.DoctorID);
            appointmentType = await appointmentTypeService.GetAppointmentTypeByIDAsync(currRequest.AppointmentTypeID);
            patientNameTextBox.Text = patient.FirstName;
            DoctorTextBox.Text = user.FirstName + " " + user.LastName;
            dpDatePicker.SelectedDate = currRequest.AppointmentTime;
            typeTextBox.Text = appointmentType.Name;
            idTextBox.Text = currRequest.BookAppointmentRequestID.ToString();


            List<string> times = new List<string>();
            for (int i = 9; i <= 18; i++) //slots here
            {
                if (i < 10)
                {
                    times.Add($"0{i}:00");
                    times.Add($"0{i}:30");
                }
                else
                {
                    times.Add($"{i}:00");
                    times.Add($"{i}:30");
                }

            }
            timeCB.ItemsSource = times;
            timeCB.SelectedItem = currRequest.AppointmentTime.ToString("HH:mm");
        }
        async void Refresh()
        {
            List<string> availTimes = await globals.GetAvailableTimeSlotsOnDayAsync(dpDatePicker.SelectedDate.Value, currRequest.DoctorID);
            if (availTimes.Count == 0)
            {
                dpDatePicker.SelectedDate.Value.AddDays(1);
            }
            timeCB.ItemsSource = availTimes;
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
                string s = currRequest.AppointmentTime.ToString("HH:mm");
                timeCB.SelectedItem = s;
                dpDatePicker.SelectedDate = currRequest.AppointmentTime;

            }
        }
        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDateTime = new DateTime(dpDatePicker.SelectedDate.Value.Year, dpDatePicker.SelectedDate.Value.Month, dpDatePicker.SelectedDate.Value.Day, selectedDateTime.Hour, selectedDateTime.Minute, 0);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private async void approveVisit(object sender, MouseButtonEventArgs e)
        {
            currRequest.AppointmentTime = selectedDateTime;
            Visit visit = new Visit
            {
                DoctorID = currRequest.DoctorID,
                PatientID = currRequest.PatientID,
                TimeStamp = currRequest.AppointmentTime,
                VisitID = IDGeneration.generateNewVisitID(currRequest.PatientID, currRequest.AppointmentTime),
                AppointmentTypeID = currRequest.AppointmentTypeID,
            };
            globals.ScheduleVisit(visit);
            VisitService visitService = new VisitService();
            if (await globals.CanBookVisit(visit))
            {
                await visitService.InsertVisitAsync(visit);
                await appointmentService.MarkAsConfirmedAsync(currRequest.BookAppointmentRequestID);
                Window.GetWindow(this).Close();

            }
            else
            {
                MessageBox.Show("Can't Book in this time slot, change slot or select first available slot.");
            }


        }


        private async void cancelVisit(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Are you sure you want to cancel this appointment?");
            if (message == MessageBoxResult.Yes)
            {
                await appointmentService.DeleteBookAppointmentRequestAsync(currRequest.BookAppointmentRequestID);
                MessageBox.Show("Appointment request has been deleted");
                Window.GetWindow(this).Close();
            }


        }

        private void viewPatient(object sender, MouseButtonEventArgs e)
        {
            
            if (patient != null)
            {
                new patientView(patient).Show();
            }

        }

        private void viewDoctor(object sender, MouseButtonEventArgs e)
        {

            if (user != null)
            {
                new patientView(user).Show();


            }
        }
    }
}
