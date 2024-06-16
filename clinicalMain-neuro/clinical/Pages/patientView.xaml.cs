using clinical.Pages;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System.Windows;
using System.Windows.Input;

namespace clinical
{
    /// <summary>
    /// Interaction logic for patientView.xaml
    /// </summary>
    public partial class patientView : Window
    {
        Patient selectedPatient;

        PatientService PatientService = new PatientService();
        public patientView(Patient patient)
        {
            InitializeComponent();
            selectedPatient = patient;
            mainFrame.Navigate(new patientViewMainPage(selectedPatient));
            sideFrame.Navigate(new DoctorSideBar());

        }
        public patientView(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            selectedPatient = PatientService.GetPatientByIdAsync(medicalRecord.PatientID).Result;
            mainFrame.Navigate(new newRecordPage(medicalRecord));
            sideFrame.Navigate(new DoctorSideBar());

        }
        public patientView(User viewDoctor)
        {
            InitializeComponent();
            mainFrame.Navigate(new reciptionistViewDoctor(viewDoctor));

            sideFrame.Navigate(new DoctorSideBar());

        }
        public patientView(Visit viewDoctor)
        {
            InitializeComponent();
            mainFrame.Navigate(new visit(viewDoctor));
            sideFrame.Navigate(new DoctorSideBar());

        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //MinWidth="90"
            //MaxWidth = "680"
            sideGrid.Width = 680;
            Width = 1380;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            sideGrid.Width = 90;
            Width = 1180;


        }
    }
}
