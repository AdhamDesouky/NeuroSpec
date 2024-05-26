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

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminSettingsSecondPage.xaml
    /// </summary>
    public partial class adminSettingsSecondPage : Page
    {
        public adminSettingsSecondPage()
        {
            InitializeComponent();
            appointmentTypesDataGrid.ItemsSource = DB.GetAllAppointmentTypes();

            //accessRequestsDataGrid.ItemsSource = DB.GetAllAccessRequests();

        }
        private void newPackage(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(3);
            form.Show();
        }
        private void newRoom(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(5);
            form.Show();
        }

        private void newEquipment(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(7);
            form.Show();
        }
        
        private void approveRequest(object sender, RoutedEventArgs e)
        {

        }

        private void viewRequest(object sender, RoutedEventArgs e)
        {

        }

        private void rejectRequest(object sender, RoutedEventArgs e)
        {

        }
        

        private void goToFirstPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminSettingsPage());

        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
            appointmentTypesDataGrid.ItemsSource = DB.GetAllAppointmentTypes();
            //accessRequestsDataGrid.ItemsSource = DB.GetAllAccessRequests();
        }

        private void newAppointmentType(object sender, MouseButtonEventArgs e)
        {
            new newPatientForm(12).Show();
        }

        private void viewAppointmentType(object sender, RoutedEventArgs e)
        {
            AppointmentType ap = (AppointmentType)appointmentTypesDataGrid.SelectedItem;
            new newPatientForm(ap).Show();
        }

        private void packagesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void delete(object sender, RoutedEventArgs e)
        {
            
            if (appointmentTypesDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this appointment type?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AppointmentType ap = (AppointmentType)appointmentTypesDataGrid.SelectedItem;
                    DB.DeleteAppointmentType(ap.TypeID);
                    appointmentTypesDataGrid.ItemsSource = DB.GetAllAppointmentTypes();
                }
            }
        }
    }
}
