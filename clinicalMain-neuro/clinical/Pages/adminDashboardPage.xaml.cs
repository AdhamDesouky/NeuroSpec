using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;
using LiveCharts.Wpf;
using LiveCharts;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminDashboardPage.xaml
    /// </summary>
    public partial class adminDashboardPage : Page
    {
        UserService UserService = new UserService();
        VisitService VisitService = new VisitService();
        PaymentService PaymentService = new PaymentService();
        public adminDashboardPage(User admin)
        {
            InitializeComponent();
            refresh();

        }
        public async void refresh()
        {
            DoctorsDataGrid.ItemsSource = await UserService.GetAllDoctorsAsync();
            employeesDataGrid.ItemsSource= await UserService.GetAllEmployeesAsync();
            //hereNowDataGrid.ItemsSource=DB.GetAllUserswRecordsByDate(DateTime.Now);
            UpdateFinancesChart();
            List<Visit> visits = await VisitService.GetVisitsByDateAsync(DateTime.Now);
            foreach (var i in visits)
            {
                appointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            }


        }

        void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                NavigationService.Navigate(new visit(visit));
            }
        }
        void viewPatient(Patient patient)
        {
            if (patient != null)
            {
                NavigationService.Navigate(new patientViewMainPage(patient));
            }
        }
        private async void UpdateFinancesChart()
        {
            List<Payment> payments = await PaymentService.GetAllPaymentsAsync();

            SeriesCollection s = new LiveCharts.SeriesCollection();
            financesChart.Series = s; // Set the series collection for the specific chart

            var distinctDoctorIds = payments.Select(payment => payment.DoctorID).Distinct();
            List<Brush> barColors = new List<Brush> { (Brush)Application.Current.Resources["lightFontColor"], (Brush)Application.Current.Resources["lighterColor"], (Brush)Application.Current.Resources["selectedColor"], Brushes.AntiqueWhite};
            int colorInd=0;

            foreach (var DoctorId in distinctDoctorIds)
            {
                var paymentsForDoctor = payments.Where(payment => payment.DoctorID == DoctorId);

                ColumnSeries columnSeries = new ColumnSeries
                {
                    Title = $"Dr. {(await UserService.GetUserByIdAsync(DoctorId)).FullName}",
                    Values = new ChartValues<double> { paymentsForDoctor.Sum(payment => payment.Amount) },
                    LabelPoint = point => point.Y.ToString("C"),
                    Fill = barColors[colorInd] // Customize the color if needed
                };
                colorInd = (colorInd + 1) % barColors.Count;
                s.Add(columnSeries);
            }

            // Set the X-axis labels dynamically based on Doctor IDs
            //financesChart.AxisX[0].Labels = distinctDoctorIds.Select(async id => $"Dr. {(await UserService.GetUserByIdAsync(id)).FullName}").ToArray();
            financesChart.AxisY[0].LabelFormatter = value => value.ToString("C"); // Use currency format if applicable


        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }


        private void newEmployee(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(2);
            window.Show();
        }

        private void newDoctor(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(1);
            window.Show();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void viewDoctor(object sender, RoutedEventArgs e)
        {
            new viewUser((User)DoctorsDataGrid.SelectedItem).Show();
        }

        private void startVisitClick(object sender, RoutedEventArgs e)
        {

        }

        private void deleteVisitClick(object sender, RoutedEventArgs e)
        {

        }

       

        private void deleteUser(object sender, RoutedEventArgs e)
        {
            User selectedUser=(User)employeesDataGrid.SelectedItem;
            if (selectedUser == null)
            {
                selectedUser = (User)DoctorsDataGrid.SelectedItem;
            }
            MessageBoxResult result=MessageBox.Show($"Are you sure you want to delete {selectedUser.FirstName} {selectedUser.LastName} ?","Delete User",MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                UserService.DeleteUserAsync(selectedUser.UserID);
                MessageBox.Show($"{selectedUser.FirstName} data has been deleted successfully.");
            }

        }
    }
}
