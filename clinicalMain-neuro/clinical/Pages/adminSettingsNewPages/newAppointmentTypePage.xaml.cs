using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace clinical.Pages.adminSettingsNewPages
{
    /// <summary>
    /// Interaction logic for newAppointmentTypePage.xaml
    /// </summary>
    public partial class newAppointmentTypePage : Page
    {

        AppointmentTypeService service = new AppointmentTypeService();
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

        private async void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string description = descriptionTextBox.Text;
            string name = NameTextBox.Text;
            int time = int.Parse(timeTextBox.Text);
            double cost = Double.Parse(costTextBox.Text); ;

            AppointmentType ap = new AppointmentType
            {
                TypeID = id,
                Description = description,
                Name = name,
                TimeInMinutes = time,
                Cost = cost
            };

            if (viewing)
            {
                await service.UpdateAppointmentTypeAsync(ap.TypeID,ap);

                MessageBox.Show("Appointment Type updated, ID: " + id.ToString());
            }
            else
            {
                await service.InsertAppointmentTypeAsync(ap);

                MessageBox.Show("New Appointment Type added, ID: " + id.ToString());
            }

            Window.GetWindow(this).Close();
        }
    }
}
