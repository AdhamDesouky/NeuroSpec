using clinical.Pages.reciptionistPages;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for ReceptionistDashboard.xaml
    /// </summary>
    public partial class ReceptionistDashboard : Page
    {
        public DateTime currentDayIndex = DateTime.Now;
        VisitService visitService = new VisitService();
        PatientService patientService = new PatientService();
        UserService userService = new UserService();
        BookAppointmentService bookAppointmentRequestService = new BookAppointmentService();
        public ReceptionistDashboard(User employee)
        {
            InitializeComponent();
            UpdateDayBorders();
            leftSideFrame.NavigationService.Navigate(new DashBoardPage());
            signedInTB.Text = $"Welcome, {employee.FirstName}";
        }

        async void updateDayAppointments()
        {
            todayAppointmentsStackPanel.Children.Clear();
            List<Visit> visits = await visitService.GetVisitsByDateAsync(currentDayIndex);
            numberOfAppointmentsTB.Text = visits.Count.ToString();

            foreach (var i in visits)
            {
                todayAppointmentsStackPanel.Children.Add(await globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            }
        }


        private void viewPatient(Patient patient)
        {

            if (patient != null)
            {
                leftSideFrame.NavigationService.Navigate(new reciptionistViewPatient(patient));
                leftSideFrame.Visibility = Visibility.Visible;
                leftSideFrame.BringIntoView();
            }

        }
        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                if (globals.signedIn.isReciptionist)
                {
                    leftSideFrame.Navigate(new reciptionistViewVisit(visit));
                    leftSideFrame.Visibility = Visibility.Visible;
                    leftSideFrame.BringIntoView();
                }
                else
                {
                    NavigationService.Navigate(new visit(visit));

                }
            }
        }


        private void newAppointment(object sender, MouseButtonEventArgs e)
        {
            newAppointmentWindow window = new newAppointmentWindow();
            window.Show();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void viewDoctor(object sender, RoutedEventArgs e)
        {
            User selectedDoctor = (User)DoctorsDataGrid.SelectedItem;
            leftSideFrame.NavigationService.Navigate(new reciptionistViewDoctor(selectedDoctor));
            leftSideFrame.Visibility = Visibility.Visible;
            leftSideFrame.BringIntoView();
            //patientView view = ;
            //view.Show();
        }


        void createDayUI(DateTime date)
        {

            Border border = new Border
            {
                Style = (Style)Application.Current.Resources["theLinedBorder"],
                Background = (Brush)Application.Current.Resources["lighterColor"],
                Margin = new Thickness(5),
                Width = 55
            };
            if (date == currentDayIndex)
            {
                border.Background = (Brush)Application.Current.Resources["selectedColor"];
            }
            border.MouseDown += (sender, e) => selectDay(date);

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(24) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            TextBlock dayTextBlock = new TextBlock
            {
                Text = date.ToString("ddd"),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            TextBlock dateTextBlock = new TextBlock
            {
                Text = date.ToString("dd"),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 21
            };

            grid.Children.Add(dayTextBlock);
            grid.Children.Add(dateTextBlock);

            Grid.SetRow(dateTextBlock, 1);

            border.Child = grid;


            dayStack.Children.Add(border);
        }

        private void selectDay(DateTime date)
        {
            currentDayIndex = date;
            UpdateDayBorders();
        }

        private void goLeftDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(-1);
            UpdateDayBorders();
        }

        private void goRightDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(1);
            UpdateDayBorders();
        }
        
        private async void UpdateDayBorders()
        {
            pendingRequestsTB.Text= (await bookAppointmentRequestService.GetNotConfirmedBookAppointmentRequestsAsync()).Count.ToString();

            if (allPanelCB.IsChecked == true && (currentDayIndex.DayOfYear != DateTime.Now.DayOfYear))
            {
                
                DoctorsDGTitleTB.Text = currentDayIndex.ToString("M") + "' Doctors";
                patientsDGTitleTB.Text = currentDayIndex.ToString("M") + "' Patients";
                selectedDayTB.Text = currentDayIndex.ToString("D");
                List<Visit>visitsOnDay = await visitService.GetVisitsByDateAsync(currentDayIndex);
                List<Patient> patients = new List<Patient>();
                List<User> Doctors = new List<User>();
                foreach (var i in visitsOnDay)
                {
                    patients.Add(await patientService.GetPatientByIdAsync(i.PatientID));
                }
                foreach(var i in visitsOnDay)
                {
                    Doctors.Add(await userService.GetUserByIdAsync(i.DoctorID));
                }
                patientsDataGrid.ItemsSource = patients;
                DoctorsDataGrid.ItemsSource = Doctors;
                numberOfDoctorsTB.Text = Doctors.Count.ToString();
            }
            else
            {
                DoctorsDGTitleTB.Text = "Today's Doctors";
                patientsDGTitleTB.Text = "Today's Patients";
                selectedDayTB.Text = currentDayIndex.ToString("M") + ", Today";
                List<Visit> visitsOnDay = await visitService.GetVisitsByDateAsync(DateTime.Now);
                List<Patient> patients = new List<Patient>();
                List<User> Doctors = new List<User>();
                foreach (var i in visitsOnDay)
                {
                    patients.Add(await patientService.GetPatientByIdAsync(i.PatientID));
                }
                foreach (var i in visitsOnDay)
                {
                    Doctors.Add(await userService.GetUserByIdAsync(i.DoctorID));
                }
                patientsDataGrid.ItemsSource = patients;
                DoctorsDataGrid.ItemsSource = Doctors;
                numberOfDoctorsTB.Text = Doctors.Count.ToString();

            }
            if (dayStack != null)
                dayStack.Children.Clear();

            //DateTime time = currentDayIndex;
            for (int i = -2; i <= 2; i++)
            {
                createDayUI(currentDayIndex.AddDays(i));
            }
            updateDayAppointments();

        }

        private void allPanelCBCheck(object sender, RoutedEventArgs e)
        {
            UpdateDayBorders();
        }



        private void dpDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDayIndex = (DateTime)dp.SelectedDate;
            UpdateDayBorders();
        }

        private void newPatient(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(0);
            window.Show();
        }

        private void viewReciptionistProfile(object sender, MouseButtonEventArgs e)
        {
            new PendingAppointments().Show();

        }

        private void viewPatient(object sender, RoutedEventArgs e)
        {
            viewPatient((Patient)patientsDataGrid.SelectedItem);
        }

        private void leftSideFrame_Navigated(object sender, NavigationEventArgs e)
        {
            updateDayAppointments();
            UpdateDayBorders();
            if (leftSideFrame.NavigationService.Equals(new DashBoardPage())) { leftSideFrame.Visibility = Visibility.Hidden; }
        }
    }
}

