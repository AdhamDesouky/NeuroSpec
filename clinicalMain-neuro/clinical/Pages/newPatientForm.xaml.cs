using clinical.Pages.adminSettingsNewPages;
using NeuroSpec.Shared.Models.DTO;
using System.Windows;
using System.Windows.Input;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientForm.xaml
    /// </summary>
    public partial class newPatientForm : Window
    {
        public newPatientForm(int type)
        {
            InitializeComponent();
            if (type == 0) //new patient
            {
                window.Height = 721;
                formTitle.Content = "New Patient";
                mainFrame.Navigate(new newPatientPage());
            }
            else if (type == 1) //new Doctor
            {
                window.Height = 440;    

                formTitle.Content = "New Physio Therapist";
                mainFrame.Navigate(new newEmployeePage(true));
            }
            else if (type == 2) //new employee
            {
                window.Height = 440;

                formTitle.Content = "New Employee";
                mainFrame.Navigate(new newEmployeePage(false));
            }
            
            //else if(type == 8) //new chronic
            //{
            //    window.Height = 271;
            //    window.Width = 400;

            //    formTitle.Content = "New Chronic Disease";
            //    mainFrame.Navigate(new newChronic());
            //}

            else if (type == 12)
            {
                window.Height = 321;
                window.Width = 420;

                formTitle.Content = "New Appointment Type";
                mainFrame.Navigate(new newAppointmentTypePage());
            }

        }
        //view patient
        public newPatientForm(Patient editable)
        {
            InitializeComponent();
            window.Height = 621;
            formTitle.Content = "Edit Patient";
            mainFrame.Navigate(new newPatientPage(editable));
        }
        

        

        //view Chronic
        //public newPatientForm(ChronicDisease toView)
        //{
        //    InitializeComponent();
        //    window.Height = 301;
        //    window.Width = 400;
        //    formTitle.Content = "View Chronic Disease";
        //    mainFrame.Navigate(new newChronic(toView));
        //}
        
        //view Appointment Type
        public newPatientForm(AppointmentType toView)
        {
            InitializeComponent();
            window.Height = 321;
            window.Width = 420;

            formTitle.Content = "View Appointment Type";
            mainFrame.Navigate(new newAppointmentTypePage(toView));
        }


        private void closeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }
        private void minimizeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window.GetWindow(this).DragMove();
            }
        }
    }
}
