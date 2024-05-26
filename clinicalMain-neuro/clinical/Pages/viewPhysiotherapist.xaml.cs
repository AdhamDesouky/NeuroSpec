using clinical.BaseClasses;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for viewDoctor.xaml
    /// </summary>
    public partial class viewDoctor : Page
    {
        User currDoctor;
        public viewDoctor(User user)
        {
            InitializeComponent();
            currDoctor=user;
            if (user == null) return;

            firstNameTextBox.Text = user.FullName;
            addressTextBox.Text = user.Address;
            genderTextBox.Text = user.Gender;
            emailTextBox.Text = user.Email;
            NIDTextBox.Text = user.NationalID;
            phoneTextBox.Text = user.PhoneNumber;
            hiringDatePicker.SelectedDate = user.HireDate;
            bdDatePicker.SelectedDate = user.Birthdate;

            firstNameTextBox.IsEnabled = false;
            addressTextBox.IsEnabled = false;
            genderTextBox.IsEnabled = false;
            emailTextBox.IsEnabled = false;
            NIDTextBox.IsEnabled = false;
            phoneTextBox.IsEnabled = false;
            hiringDatePicker.IsEnabled = false;
            bdDatePicker.IsEnabled = false;
            
            List<Patient> patients = DB.GetAllPatientsByDoctorID(user.UserID);
            patientsDataGrid.ItemsSource= patients;

            List<Visit> visits = DB.GetAllVisitsByDoctorID(user.UserID);
            foreach(var i in visits)
            {
                appointmentsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            }



            UpdateFinancesChart();
            UpdateAttendanceChart();

        }


        private void UpdateAttendanceChart()
        {
            List<AttendanceRecord> attendanceRecords = DB.GetUserAttendanceRecords(currDoctor.UserID);

            SeriesCollection s = new LiveCharts.SeriesCollection();
            attendanceChart.Series = s;

            IEnumerable<IGrouping<int, AttendanceRecord>> groupedDataWeek;
            IEnumerable<IGrouping<DateTime, AttendanceRecord>> groupedDataMonth;

            // Group attendance records by week
            groupedDataWeek = attendanceRecords
                .GroupBy(record => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .OrderBy(group => group.Key);

            ColumnSeries weekSeries = new ColumnSeries
            {
                Title = "Weekly Attendances",
                Values = new ChartValues<int>(groupedDataWeek.Select(group => group.Count())),
                LabelPoint = point => point.Y + " attendances",
                Fill = Brushes.Blue
            };

            s.Add(weekSeries);

            // Group attendance records by month
            groupedDataMonth = attendanceRecords
                .GroupBy(record => new DateTime(record.TimeStamp.Year, record.TimeStamp.Month, 1))
                .OrderBy(group => group.Key);

            ColumnSeries monthSeries = new ColumnSeries
            {
                Title = "Monthly Attendances",
                Values = new ChartValues<int>(groupedDataMonth.Select(group => group.Count())),
                LabelPoint = point => point.Y + " attendances",
                Fill = Brushes.Green
            };

            s.Add(monthSeries);

            // Set the X-axis labels dynamically based on the selected time unit
            attendanceChart.AxisX[0].Labels = attendanceRecords
                .Select(record => $"Week {CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}")
                .Distinct()
                .OrderBy(week => int.Parse(week.Substring(5)))
                .Concat(attendanceRecords
                    .Select(record => record.TimeStamp.ToString("MMM"))
                    .Distinct()
                    .OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month))
                .ToArray();

            attendanceChart.AxisY[0].LabelFormatter = value => value.ToString(); // Use integer format for count
        }



        private void UpdateFinancesChart()
        {
            List<Payment> payments = DB.GetDoctorPayments(currDoctor.UserID); // Assuming currDoctor is the current Doctor

            var distinctPatientIds = payments.Select(payment => payment.PatientID).Distinct();

            SeriesCollection s = new LiveCharts.SeriesCollection();
            financesChart.Series = s; // Set the series collection for the specific chart

            foreach (var patientId in distinctPatientIds)
            {
                var paymentsForPatient = payments.Where(payment => payment.PatientID == patientId);

                IEnumerable<IGrouping<DateTime, Payment>> groupedData;

                // Assuming you want to group payments by month
                groupedData = paymentsForPatient
                    .GroupBy(payment => new DateTime(payment.TimeStamp.Year, payment.TimeStamp.Month, 1))
                    .OrderBy(group => group.Key);

                LineSeries lineSeries = new LineSeries
                {
                    Title = $"{DB.GetPatientById(patientId).FullName}",
                    Values = new ChartValues<double>(groupedData.SelectMany(group => group.Select(payment => payment.Amount))),
                    PointGeometry = null // This removes the point marker
                };

                s.Add(lineSeries);
            }

            // Set the X-axis labels dynamically based on the selected time unit
            financesChart.AxisX[0].Labels = payments.Select(payment => payment.TimeStamp.ToString("MMM")).Distinct().OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month).ToArray();
            financesChart.AxisY[0].LabelFormatter = value => value.ToString("C"); // Use currency format if applicable
        }
        void viewVisit(Visit visit) {
            if (visit != null)
            {
                NavigationService.Navigate(new visit(visit));
            }
        }
        void viewPatient(Patient patient) {
            if (patient != null)
            {
                NavigationService.Navigate(new patientViewMainPage(patient));
            }
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
