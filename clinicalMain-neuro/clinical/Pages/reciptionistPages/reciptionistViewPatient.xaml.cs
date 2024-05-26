using clinical.BaseClasses;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clinical.Pages.reciptionistPages
{
    /// <summary>
    /// Interaction logic for reciptionistViewPatient.xaml
    /// </summary>
    public partial class reciptionistViewPatient : Page
    {
        
        

        Patient patient;

        public reciptionistViewPatient(Patient toViewPatient)
        {
            InitializeComponent();
            //if (NavigationService==null||!NavigationService.CanGoBack) goBackIcon.Visibility = Visibility.Hidden;
            patient = toViewPatient;
            NameMainTxt.Text = patient.FullName;
            nameTextBox.Text = patient.FullName;
            emailTextBox.Text = patient.Email;
            phoneTextBox.Text = patient.PhoneNumber;
            addressTextBox.Text = patient.Address;
            genderTB.Text = patient.Gender;
            idTextBox.Text = patient.PatientID.ToString();
            payTextBox.Text = "0";
            referringTextBox.Text = patient.referringName+$", {patient.referringPN}";
            ageTB.Text = patient.Age.ToString();

            List<User> Doctors = DB.GetAllDoctors();
            DoctorCB.ItemsSource=Doctors;
            for(int i = 0; i < Doctors.Count; i++)
            {
                if (patient.DoctorID == Doctors[i].UserID)
                {
                    DoctorCB.SelectedIndex = i; break;
                }
            }
            dueTextBox.Text = patient.DueAmount.ToString();

            handleFinances();
            

            List<Payment> payments = DB.GetPatientPayments(patient.PatientID);
            financesDatagrid.ItemsSource = payments;

            List<Visit> upcomingVisits = DB.GetPatientUpcomingVisits(patient.PatientID);
            List<Visit> previousVisits = DB.GetPatientPrevVisits(patient.PatientID);
            foreach (var i in upcomingVisits)
            {
                upcomingAppointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit));
            }
            foreach (var i in previousVisits)
            {
                previousAppointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit));
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
            if(NavigationService.CanGoBack)NavigationService.GoBack();

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
            DoctorCB.IsEnabled= !DoctorCB.IsEnabled;
        }

        private void syncPatientInfo(object sender, MouseButtonEventArgs e)
        {
            string firstName = nameTextBox.Text.Split(" ")[0].Trim();
            string lastName = nameTextBox.Text.Split(" ")[1].Trim();
            string em=emailTextBox.Text.Trim();
            string pn = phoneTextBox.Text.Trim();
            double due = Double.Parse(dueTextBox.Text.Trim());
            int physId = ((User)DoctorCB.SelectedItem).UserID;
            int age = int.Parse(ageTB.Text);
            string address = addressTextBox.Text;
            string referringName= referringTextBox.Text.Split(",")[0].Trim();
            string referringPN= referringTextBox.Text.Split(",")[1].Trim();
            string gender=genderTB.Text;
            Patient newP = patient;
            newP.FirstName = firstName;
            newP.LastName = lastName;
            newP.Email = em;
            newP.PhoneNumber=pn;
            newP.DueAmount=due;
            newP.DoctorID=physId;
            newP.Address=address;
            newP.referringName=referringName;
            newP.referringPN=referringPN;
            newP.Gender=gender;
            newP.Birthdate = globals.CalculateBirthdate(age);
            DB.UpdatePatient(newP);

            editPatientInfo(sender,e);//if error it's here
        }

        private void viewPaymentFromGrid(object sender, RoutedEventArgs e)
        {

        }

        private void payNow(object sender, MouseButtonEventArgs e)
        {
            handleFinances();
            double toBePaid = Double.Parse(payTextBox.Text);
            double patientDue= Double.Parse(dueTextBox.Text);

            MessageBoxResult result = MessageBox.Show($"Register payment of {toBePaid} for {patient.FullName}?","Payment Confirmation",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DB.InsertPayment(new Payment(globals.generateNewPaymentID(patient.PatientID,DateTime.Now), toBePaid,DateTime.Now,patient.DoctorID,patient.PatientID));
                patient.DueAmount = patientDue;
                DB.UpdatePatient(patient);
            }
        }



        void handleFinances()
        {
            

            if (patient != null) //selected a patient? YES
            {
                    double patientDueAmount = patient.DueAmount;

                    if (payTextBox.Text != null && payTextBox.Text != "")
                    {
                        patientDueAmount -= Double.Parse(payTextBox.Text.Trim());
                    }

                    dueTextBox.Text = patientDueAmount.ToString();


            }
        }

        private void payTextChanged(object sender, TextChangedEventArgs e)
        {
            handleFinances();
        }

        private void doctorSelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

