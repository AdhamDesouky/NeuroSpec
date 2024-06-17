using NeuroSpec.Shared.Globals;
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
    /// Interaction logic for reciptionistViewPatient.xaml
    /// </summary>
    public partial class reciptionistViewPatient : Page
    {
        Patient patient;
        UserService userService = new UserService();
        PaymentService paymentService = new PaymentService();
        VisitService visitService = new VisitService();
        PatientService patientService = new PatientService();
        public reciptionistViewPatient(Patient toViewPatient)
        {
            InitializeComponent();
            //if (NavigationService==null||!NavigationService.CanGoBack) goBackIcon.Visibility = Visibility.Hidden;
            patient = toViewPatient;
            NameMainTxt.Text = patient.FirstName+" "+ patient.LastName;
            nameTextBox.Text = patient.FirstName + " " + patient.LastName;
            emailTextBox.Text = patient.Email;
            phoneTextBox.Text = patient.PhoneNumber;
            if(patient.Address!=null)
            addressTextBox.Text = patient.Address.ToString();
            genderTB.Text = patient.Gender?"Male":"Female";
            idTextBox.Text = patient.PatientID.ToString();
            payTextBox.Text = "0";
            referringTextBox.Text = patient.ReferringDoctor;
            ageTB.Text = StaticFunctions.CalculateAge(patient.DateOfBirth).ToString();

            
            //dueTextBox.Text = patient.DueAmount.ToString();

            //handleFinances();
            initAsync();

            //List<Payment> payments = paymentService.get(patient.PatientID);
            //financesDatagrid.ItemsSource = payments;

            
        }

        async void initAsync()
        {
            List<Visit> upcomingVisits = await visitService.GetVisitsByPatientIDAsync(patient.PatientID);
            //List<Visit> previousVisits = DB.GetPatientPrevVisits(patient.PatientID);
            foreach (var i in upcomingVisits)
            {
                upcomingAppointmentsStackPanel.Children.Add(await globals.createAppointmentUIObject(i, viewVisit));
            }
            foreach (var i in upcomingVisits)
            {
                previousAppointmentsStackPanel.Children.Add(await globals.createAppointmentUIObject(i, viewVisit));
            }

            List<User> Doctors = await userService.GetAllDoctorsAsync();
            DoctorCB.ItemsSource = Doctors;
            for (int i = 0; i < Doctors.Count; i++)
            {
                if (patient.AssignedDoctorID == Doctors[i].UserID)
                {
                    DoctorCB.SelectedIndex = i; break;
                }
            }
        }

        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                NavigationService.Navigate(new reciptionistViewVisit(visit));
            }
        }



        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();

        }

        private void editPatientInfo(object sender, MouseButtonEventArgs e)
        {
            nameTextBox.IsEnabled = !nameTextBox.IsEnabled;
            emailTextBox.IsEnabled = !emailTextBox.IsEnabled;
            phoneTextBox.IsEnabled = !phoneTextBox.IsEnabled;
            dueTextBox.IsEnabled = !dueTextBox.IsEnabled;
            genderTB.IsEnabled = !genderTB.IsEnabled;
            addressTextBox.IsEnabled = !addressTextBox.IsEnabled;
            referringTextBox.IsEnabled = !referringTextBox.IsEnabled;
            ageTB.IsEnabled = !ageTB.IsEnabled;
            DoctorCB.IsEnabled = !DoctorCB.IsEnabled;
        }

        private async void syncPatientInfo(object sender, MouseButtonEventArgs e)
        {
            string firstName = nameTextBox.Text.Split(" ")[0].Trim();
            string lastName = nameTextBox.Text.Split(" ")[1].Trim();
            string em = emailTextBox.Text.Trim();
            string pn = phoneTextBox.Text.Trim();
            double due = Double.Parse(dueTextBox.Text.Trim());
            int physId = ((User)DoctorCB.SelectedItem).UserID;
            int age = int.Parse(ageTB.Text);
            string address = addressTextBox.Text;
            string referringName = referringTextBox.Text.Split(",")[0].Trim();
            string referringPN = referringTextBox.Text.Split(",")[1].Trim();
            string gender = genderTB.Text;
            Patient newP = patient;
            newP.FirstName = firstName;
            newP.LastName = lastName;
            newP.Email = em;
            newP.PhoneNumber = pn;
            //newP.DueAmount = due;
            newP.AssignedDoctorID = physId;
            newP.Address = new Address();
            newP.Address.Street= address;
            newP.ReferringDoctor = referringName;
            //newP.referringPN = referringPN;
            newP.Gender = gender=="Male";
            newP.DateOfBirth = StaticFunctions.CalculateBirthdate(age);

            await patientService.UpdatePatientAsync(newP);

            editPatientInfo(sender, e);//if error it's here
        }

        private void viewPaymentFromGrid(object sender, RoutedEventArgs e)
        {

        }

        private async void payNow(object sender, MouseButtonEventArgs e)
        {
            //handleFinances();
            double toBePaid = Double.Parse(payTextBox.Text);
            double patientDue = Double.Parse(dueTextBox.Text);

            MessageBoxResult result = MessageBox.Show($"Register payment of {toBePaid} for {patient.FirstName+" "+patient.LastName}?", "Payment Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                await paymentService.InsertPaymentAsync(new Payment { PaymentID = IDGeneration.generateNewPaymentID(patient.PatientID, DateTime.Now), Amount = toBePaid, TimeStamp = DateTime.Now, DoctorID = (int)patient.AssignedDoctorID, PatientID = patient.PatientID });
                //patient.DueAmount = patientDue;
                //DB.UpdatePatient(patient);
            }
        }



        //void handleFinances()
        //{


        //    if (patient != null) //selected a patient? YES
        //    {
        //        //double patientDueAmount = patient.DueAmount;

        //        if (payTextBox.Text != null && payTextBox.Text != "")
        //        {
        //            patientDueAmount -= Double.Parse(payTextBox.Text.Trim());
        //        }

        //        dueTextBox.Text = patientDueAmount.ToString();


        //    }
        //}

        private void payTextChanged(object sender, TextChangedEventArgs e)
        {
            //handleFinances();
        }

        private void doctorSelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

