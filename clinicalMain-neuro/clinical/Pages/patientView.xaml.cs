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
        MedicalRecord medicalRecord;
        PatientService PatientService = new PatientService();
        public patientView(Patient patient)
        {
            InitializeComponent();
            selectedPatient = patient;
            mainFrame.Navigate(new patientViewMainPage(selectedPatient));

        }
        public patientView(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
            initAsync();
            mainFrame.Navigate(new newRecordPage(medicalRecord));

        }
        async void initAsync()
        {
            selectedPatient = await PatientService.GetPatientByIdAsync(medicalRecord.PatientID);

        }
        public patientView(User viewDoctor)
        {
            InitializeComponent();
            mainFrame.Navigate(new reciptionistViewDoctor(viewDoctor));


        }
        public patientView(Visit viewDoctor)
        {
            InitializeComponent();
            mainFrame.Navigate(new visit(viewDoctor));

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

       
    }
}
