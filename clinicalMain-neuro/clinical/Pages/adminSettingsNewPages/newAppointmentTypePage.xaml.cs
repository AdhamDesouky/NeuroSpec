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
    /// Interaction logic for newAppointmentTypePage.xaml
    /// </summary>
    public partial class newAppointmentTypePage : Page
    {
        public newAppointmentTypePage()
        {
            InitializeComponent();
            idTextBox.Text = new Random().Next(100).ToString();
        }
        bool viewing = false;
        public newAppointmentTypePage(AppointmentType type)
        {
            InitializeComponent();
            viewing = true;
            idTextBox.IsEnabled = false;

            idTextBox.Text = type.TypeID.ToString();
            descriptionTextBox.Text = type.Description;
            NameTextBox.Text = type.Name;
            timeTextBox.Text = type.TimeInMinutes.ToString();
            costTextBox.Text = type.Cost.ToString();
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string description = descriptionTextBox.Text;
            string name = NameTextBox.Text;
            int time = int.Parse(timeTextBox.Text);
            double cost = Double.Parse(costTextBox.Text); ;

            AppointmentType ap = new AppointmentType(id,name,description,time,cost);

            if (viewing)
            {
                DB.UpdateAppointmentType(ap);

                MessageBox.Show("Appointment Type updated, ID: " + id.ToString());
            }
            else
            {
                DB.InsertAppointmentType(ap);

                MessageBox.Show("New Appointment Type added, ID: " + id.ToString());
            }

            Window.GetWindow(this).Close();
        }
    }
}
