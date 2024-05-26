using clinical.BaseClasses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static clinical.BaseClasses.ontology;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for adminSettingsPage.xaml
    /// </summary>
    public partial class adminSettingsPage : Page
    {
        public adminSettingsPage()
        {
            InitializeComponent();
            ontology oi = new ontology();
            chronicsDataGrid.ItemsSource = oi.GetFirstTenOntologies();
            testsDataGrid.ItemsSource=DB.GetAllTests();

        }

        private void newTreatmentPlan(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(6);
            form.Show();
        }

        

        private void newExercise(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(9);
            form.Show();
        }
        

        private void newInjury(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(4);
            form.Show();
        }

        private void newTest(object sender, MouseButtonEventArgs e)
        {
            newPatientForm form = new newPatientForm(11);
            form.Show();
        }

        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////

        

        private void viewTest(object sender, RoutedEventArgs e)
        {
            EvaluationTest test = (EvaluationTest)testsDataGrid.SelectedItem;
            newPatientForm form = new newPatientForm(test);
            form.Show();
        }
        private void goToSecondPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminSettingsSecondPage());

        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();

            chronicsDataGrid.Items.Refresh();
            testsDataGrid.Items.Refresh();

        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (testsDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this test?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    EvaluationTest test = (EvaluationTest)testsDataGrid.SelectedItem;
                    DB.DeleteTest(test.TestID);
                    testsDataGrid.Items.Refresh();
                }
            }
        }
    }
}
