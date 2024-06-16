using CommunityToolkit.HighPerformance;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using NeuroSpec.Shared.Globals;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using NeuroSpec.Shared.Services.OntologyServices;
using NeuroSpec.Shared.Models.Ontology;
using NeuroSpec.Shared.Services.DTO_Services;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        bool edit = false;
        Patient patient;
        List<SNOMEDOntology> selectedChronics = new List<SNOMEDOntology>();
        UserService us = new UserService();
        PatientService ps = new PatientService();
        PaymentService paymentService = new PaymentService();
        PatientChronicService pcs = new PatientChronicService(); 
        private ICollectionView injuryDataView;
        private ICollectionView chronicDataView;


        public newPatientPage(Patient toEdit)
        {
            InitializeComponent();
            if (toEdit == null) edit = false;
            else { edit = true; patient = toEdit; }

            if (edit && toEdit != null)
            {
                firstNameTextBox.Text = toEdit.FirstName;
                lastNameTextBox.Text = toEdit.LastName;
                maleRB.IsChecked = toEdit.Gender;
                if(toEdit.Address!=null)
                addressTextBox.Text = toEdit.Address.ToString();
                phoneTextBox.Text = toEdit.PhoneNumber;
                ageTextBox.Text = StaticFunctions.CalculateAge(toEdit.DateOfBirth).ToString();
            }
        }
        public newPatientPage()
        {
            InitializeComponent();
            asyncInit();
            assignedPhys.SelectedIndex = 0;
            selectedChronicsDataGrid.ItemsSource = selectedChronics;
            referredCB.Checked += CheckBox_Checked;
            referredCB.Unchecked += CheckBox_Unchecked;

            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;

            searchChronicDiseaseTXTBOX.TextChanged += SearchChronicTextBox_TextChanged;


        }
        private async void asyncInit()
        {
            List<User>doctors=await us.GetAllDoctorsAsync();
            assignedPhys.ItemsSource = doctors;
        }
        SNOMEDOntologyService ontologyService = new SNOMEDOntologyService();
        private async void SearchChronicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchChronicDiseaseTXTBOX.Text.Length < 4)
            {
                return;
            }
            List<SNOMEDOntology>searchResults=await ontologyService.SearchSNOMEDOntologyAsync(searchChronicDiseaseTXTBOX.Text);
            if (searchResults.Count == 0)
            {
                return;
            }
            
            allChronicsDataGrid.ItemsSource = searchResults;
            //chronicDataView.Filter = item => FilterItem(item, searchChronicDiseaseTXTBOX.Text);

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
        void refresh()
        {
            selectedChronicsDataGrid.Items.Refresh();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            referringTextBox.IsEnabled = true;
            referringPNTextBox.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;
        }

        private async void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln = lastNameTextBox.Text;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address = addressTextBox.Text;
            string phone = phoneTextBox.Text;
            DateTime bd = StaticFunctions.CalculateBirthdate(int.Parse(ageTextBox.Text));
            string em = emailTextBox.Text;
            User phys = (User)(assignedPhys.SelectedItem);
            bool isRef = (bool)referredCB.IsChecked;
            bool prevSessions = (bool)prevSessionsCB.IsChecked;
            string refName = "", refPN = "";
            if (isRef)
            {
                refName = referringTextBox.Text;
                refPN = referringPNTextBox.Text;

            }
            int id = IDGeneration.generateNewPatientID(phone);


            double due = Double.Parse(dueTB.Text);

            Patient newPatient = new Patient
            {
                PatientID = id,
                FirstName = fn,
                LastName = ln,
                DateOfBirth = bd,
                Gender = gender == "Male",
                PhoneNumber = phone,
                Email = em,
                Address = new Address
                {
                    Street = address
                },
                Height = Convert.ToDouble(heightTextBox.Text),
                Weight = Convert.ToDouble(weightTextBox.Text),
                DominantHand = true
            };
            await ps.InsertPatientAsync(newPatient);            


            MessageBox.Show("New patient added, ID: " + id.ToString());

            foreach (var ch in selectedChronics)
            {
                PatientChronic newPc=new PatientChronic
                {
                    ChronicName = ch.SNOMEDName,
                    ChronicDescription=ch.SNOMEDID,
                    PatientID = id
                };
                await pcs.InsertPatientChronicAsync(newPc);
            }

            double paid= Double.Parse(paidTB.Text);
            Payment payment = new Payment
            {
                PaymentID = IDGeneration.generateNewPaymentID(id, DateTime.Now),
                Amount = paid,
                TimeStamp = DateTime.Now,
                DoctorID = phys.UserID,
                PatientID = id
            };
            await paymentService.InsertPaymentAsync(payment);


            Window.GetWindow(this).Close();



        }



        private void removeChronic(object sender, RoutedEventArgs e)
        {
            selectedChronics.Remove((SNOMEDOntology)selectedChronicsDataGrid.SelectedItem);
            refresh();

        }

        private void addChronic(object sender, RoutedEventArgs e)
        {
            SNOMEDOntology selectedChronic = (SNOMEDOntology)allChronicsDataGrid.SelectedItem;
            if (!selectedChronics.Contains(selectedChronic)) selectedChronics.Add(selectedChronic);
            refresh();
        }
        private void paidTC(object sender, TextChangedEventArgs e)
        {
            if (paidTB.Text != null && paidTB.Text != "")
                dueTB.Text = (Double.Parse(paidTB.Text.Trim())).ToString();
        }

    }
}
