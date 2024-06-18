using MahApps.Metro.IconPacks;
using MySqlX.XDevAPI.Relational;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for reciptionistViewDoctor.xaml
    /// </summary>
    public partial class reciptionistViewDoctor : Page
    {
        User Doctor;
        PatientService patientService = new PatientService();  
        VisitService visitService = new VisitService();
        public reciptionistViewDoctor(User doctor)
        {
            InitializeComponent();
            Doctor = doctor;
            DoctorNameMainTxt.Text = Doctor.FullName;
            nameTextBox.Text = Doctor.FullName;
            emailTextBox.Text = Doctor.Email;
            phoneTextBox.Text = Doctor.PhoneNumber;
            initAsync();
            

            
        }
        async void initAsync()
        {

            List<Patient> patientList = await patientService.GetPatientsByDoctorAsync(Doctor.UserID);

            patientsDataGrid.ItemsSource = patientList;
            List<Visit> DoctorUpcomingVisits = await visitService.GetFutureDoctorVisitsAsync(Doctor.UserID);
            foreach (var i in DoctorUpcomingVisits)
            {
                upcomingAppointmentsStackPanel.Children.Add(await globals.createAppointmentUIObject(i, viewVisit, viewPatient));


            }
        }
        private void viewPatient(Patient patient)
        {

            if (patient != null)
            {
                new patientView(patient).Show();
            }

        }
        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                new patientView(visit).Show();
            }
        }

        private void viewPatientFromGrid(object sender, RoutedEventArgs e)
        {
            viewPatient((Patient)patientsDataGrid.SelectedItem);
        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}