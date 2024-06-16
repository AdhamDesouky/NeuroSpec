using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace clinical
{
    /// <summary>
    /// Interaction logic for newAppointmentWindow.xaml
    /// </summary>
    public partial class newAppointmentWindow : Window
    {
        private ICollectionView dataView;
        Patient selectedPatient = null;
        User selectedDoctor = null;
        DateTime selectedDateTime = DateTime.Now;
        DateTime lastSelectedDT = DateTime.Now;
        AppointmentType selectedType;

        List<string> times = new List<string>();
        PatientService patientService = new PatientService();
        UserService userService = new UserService();
        AppointmentTypeService appointmentTypeService = new AppointmentTypeService();
        public newAppointmentWindow()
        {
            InitializeComponent();

            for (int i = 9; i <= 18; i++) //slots here //todo: get from db
            {
                times.Add($"{i}:00");
                times.Add($"{i}:30");

            }

            List<Patient> list = patientService.GetAllPatientsAsync().Result;
            foreach (Patient pat in list)
            {
                User phys;
                if(pat.AssignedDoctorID != null)
                    phys = userService.GetUserByIdAsync((int)pat.AssignedDoctorID).Result;
            }
            allPatientsDataGrid.ItemsSource = list;
            dataView = CollectionViewSource.GetDefaultView(allPatientsDataGrid.ItemsSource);
            textBoxFilter.TextChanged += textBoxFilter_TextChanged;

            datePicker.SelectedDate = DateTime.Now;
            List<AppointmentType> types = appointmentTypeService.GetAllAppointmentTypesAsync().Result;

            visitTypeCB.ItemsSource = types;
            timePicker.ItemsSource = times;
            timePicker.SelectedIndex = 0;
            visitTypeCB.SelectedIndex = 0;


            DoctorPicker.ItemsSource = userService.GetAllDoctorsAsync().Result;

        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private void view(object sender, RoutedEventArgs e)
        {
            selectedPatient = (Patient)allPatientsDataGrid.SelectedItem;
            patientName.Text = selectedPatient.FirstName + " " + selectedPatient.LastName;
            selectedDoctor = userService.GetUserByIdAsync((int)selectedPatient.AssignedDoctorID).Result;

            DoctorName.Text = selectedDoctor.FullName;
            handleFinances();
            Refresh();


        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataView.Filter = item => FilterItem(item, textBoxFilter.Text);

        }

        private bool FilterItem(object item, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return true; // No filter, show all items
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var cellValue = property.GetValue(item);
                if (cellValue != null && cellValue.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Found a match in any column
                }
            }

            return false; // No match found
        }
        PaymentService paymentService = new PaymentService();
        private async void saveClicked(object sender, MouseButtonEventArgs e)
        {
            if (selectedPatient == null) { return; }
            MessageBoxResult result = MessageBox.Show("Book " + selectedPatient.FirstName + " a reservation on " + selectedDateTime.ToString("g"), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                int id = IDGeneration.generateNewVisitID(selectedPatient.PatientID, selectedDateTime);
                AppointmentType ap = (AppointmentType)visitTypeCB.SelectedItem;
                Visit visit = new Visit { 
                    VisitID = id, 
                    DoctorID = selectedDoctor.UserID, 
                    PatientID = selectedPatient.PatientID, 
                    TimeStamp = selectedDateTime, 
                    TherapistNotes = "",
                    Height = selectedPatient.Height, 
                    Weight = selectedPatient.Weight, 
                    AppointmentTypeID= ap.TypeID
                };
                
                globals.ScheduleVisit(visit);

                //updating patient

                //selectedPatient.DueAmount = Double.Parse(patientDueTB.Text);

                //DB.UpdatePatient(selectedPatient);

                double paid = Double.Parse(paidTB.Text.Trim());

                Payment payment = new Payment 
                {
                    PaymentID = IDGeneration.generateNewPaymentID(selectedPatient.PatientID, DateTime.Now), 
                    Amount = paid, 
                    TimeStamp = DateTime.Now, 
                    DoctorID = selectedDoctor.UserID, 
                    PatientID = selectedPatient.PatientID 
                };
                    
                await paymentService.InsertPaymentAsync(payment);
                Window.GetWindow(this).Close();
            }



        }

        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelectedDT = selectedDateTime;
            selectedDateTime = (DateTime)datePicker.SelectedDate;
            Refresh();
        }

        private void timeChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)timePicker.SelectedItem == null) return;
            string s = (string)timePicker.SelectedItem;
            string hr = "", min = "";
            hr += s[0];
            hr += s[1];
            min += s[3];
            min += s[4];

            lastSelectedDT = selectedDateTime;
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, int.Parse(hr), int.Parse(min), 0);

        }
        private void first_avail(object sender, RoutedEventArgs e)
        {

            if (selectedPatient == null || selectedDoctor == null)
            {
                return;
            }
            DateTime firstFree = globals.FindFirstFreeSlot(selectedDoctor.UserID, DateTime.Today);
            datePicker.IsEnabled = false;
            timePicker.IsEnabled = false;
            datePicker.SelectedDate = firstFree;
            timePicker.SelectedItem = firstFree.ToString("HH:mm");

            Refresh();
        }

        private void not_first_avail(object sender, RoutedEventArgs e)
        {
            selectedDateTime = lastSelectedDT;
            datePicker.IsEnabled = true;
            timePicker.IsEnabled = true;
        }

        private void DoctorChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDoctor = (User)(DoctorPicker.SelectedItem);
            DoctorName.Text = "Dr. " + selectedDoctor.FirstName + " " + selectedDoctor.LastName;
            Refresh();
        }

        private void typeChanged(object sender, SelectionChangedEventArgs e)
        {

            handleFinances();
        }

        void Refresh()
        {
            if (selectedDoctor == null) return;
            List<string> availTimes = globals.GetAvailableTimeSlotsOnDayAsync(datePicker.SelectedDate.Value, selectedDoctor.UserID);
            if (availTimes.Count == 0)
            {
                datePicker.SelectedDate.Value.AddDays(1);
            }
            timePicker.ItemsSource = availTimes;
        }

        //current visit due amount:
        //1- patient has a package active, thus all visits are rendered paid and only patient due amount is displayed
        //2- patient doesn't have an active package, thus each visit is controlled by visit type, and patient due amount is calculated by: patientDue+ visitDue

        void handleFinances()
        {
            selectedType = (AppointmentType)visitTypeCB.SelectedItem;
            if (selectedPatient != null) //selected a patient? YES
            {
                {
                    double price = selectedType.Cost;
                    double patientDueAmount = price;
                    double visitDueAmount = price;

                    if (paidTB.Text != null && paidTB.Text != "")
                    {
                        patientDueAmount -= Double.Parse(paidTB.Text.Trim());
                        visitDueAmount -= Double.Parse(paidTB.Text.Trim());
                    }

                    visitDueTB.Text = visitDueAmount.ToString();
                    patientDueTB.Text = patientDueAmount.ToString();
                }
            }
        }

        private void paidTxtChanged(object sender, TextChangedEventArgs e)
        {
            handleFinances();
        }
    }
}
