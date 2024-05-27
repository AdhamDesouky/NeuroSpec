using CommunityToolkit.HighPerformance;
using NeuroSpec.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        bool edit = false;
        Patient patient;
        List<OntologyTerm> selectedChronics = new List<OntologyTerm>();

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
                if (toEdit.Gender == "Male") maleRB.IsChecked = true;
                else maleRB.IsChecked = false;
                addressTextBox.Text = toEdit.Address;
                phoneTextBox.Text = toEdit.PhoneNumber;
                ageTextBox.Text = toEdit.Age.ToString();
            }
        }
        public newPatientPage()
        {
            InitializeComponent();

            assignedPhys.ItemsSource = DB.GetAllDoctors();
            ontology oi=new ontology();
            allChronicsDataGrid.ItemsSource = oi.GetFirstTenOntologies();
            assignedPhys.SelectedIndex = 0;
            selectedChronicsDataGrid.ItemsSource = selectedChronics;
            referredCB.Checked += CheckBox_Checked;
            referredCB.Unchecked += CheckBox_Unchecked;

            referringTextBox.IsEnabled = false;
            referringPNTextBox.IsEnabled = false;



            chronicDataView = CollectionViewSource.GetDefaultView(DB.GetAllTerms());
            searchChronicDiseaseTXTBOX.TextChanged += SearchChronicTextBox_TextChanged;


        }

        private void SearchChronicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchChronicDiseaseTXTBOX.Text.Length < 4)
            {
                return;
            }
            allChronicsDataGrid.ItemsSource = DB.GetTermsLikeName(searchChronicDiseaseTXTBOX.Text);
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

        private void save(object sender, MouseButtonEventArgs e)
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
            DateTime bd = globals.CalculateBirthdate(int.Parse(ageTextBox.Text));
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
            int id = globals.generateNewPatientID(phone);


            double due = Double.Parse(dueTB.Text);

            Patient newPatient = new(
                id,
                fn,
                ln,
                bd,
                gender,
                phone,
                em,
                address,
                phys.UserID,
                isRef,
                prevSessions,
                Convert.ToDouble(heightTextBox.Text),
                Convert.ToDouble(weightTextBox.Text),
                due,
                refName,
                refPN);
            DB.InsertPatient(newPatient);


            MessageBox.Show("New patient added, ID: " + id.ToString());

            foreach (var ch in selectedChronics)
            {
                DB.InsertPatientDiseases(ch.Name, newPatient.PatientID);
            }

            double paid= Double.Parse(paidTB.Text);
            Payment payment = new Payment(globals.generateNewPaymentID(id, DateTime.Now), paid, DateTime.Now, phys.UserID, id);
            DB.InsertPayment(payment);


            Window.GetWindow(this).Close();



        }



        private void removeChronic(object sender, RoutedEventArgs e)
        {
            selectedChronics.Remove((OntologyTerm)selectedChronicsDataGrid.SelectedItem);
            refresh();

        }

        private void addChronic(object sender, RoutedEventArgs e)
        {
            OntologyTerm selectedChronic = (OntologyTerm)allChronicsDataGrid.SelectedItem;
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
