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

namespace clinical.Pages.adminSettingsNewPages
{
    /// <summary>
    /// Interaction logic for newEvaluationTestPage.xaml
    /// </summary>
    public partial class newEvaluationTestPage : Page
    {
        public newEvaluationTestPage()
        {
            InitializeComponent();
            IDTextBox.Text=new Random().Next(1000).ToString();

        }
        bool viewing = false;
        public newEvaluationTestPage(EvaluationTest ev) 
        {
            InitializeComponent();
            IDTextBox.Text = ev.TestID.ToString();
            NameTextBox.Text = ev.TestName.ToString();
            IDTextBox.IsEnabled = false;
            viewing = true;
            descriptionTextBox.Text=ev.TestDescription.ToString();



        }
        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(IDTextBox.Text.Trim());
            string name = NameTextBox.Text.Trim();
            string desc=descriptionTextBox.Text.Trim();

            EvaluationTest ev=new EvaluationTest(id, name, desc);
            if (viewing){
                DB.UpdateTest(ev);
                MessageBox.Show("Test updated, ID: " + id.ToString());

            }
            else
            {
                DB.InsertTest(ev);
                MessageBox.Show("Test Added, ID: " + id.ToString());
            }
        }
    }
}
