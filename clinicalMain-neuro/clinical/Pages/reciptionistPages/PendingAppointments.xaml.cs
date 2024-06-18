using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
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
using System.Windows.Shapes;

namespace clinical.Pages.reciptionistPages
{
    /// <summary>
    /// Interaction logic for PendingAppointments.xaml
    /// </summary>
    public partial class PendingAppointments : Window
    {
        BookAppointmentService bookAppointmentService;
        public PendingAppointments()
        {
            InitializeComponent();
            bookAppointmentService = new BookAppointmentService();
            initAsync();
        }
        async void initAsync()
        {
            List<BookAppointmentRequest> requests = await bookAppointmentService.GetNotConfirmedBookAppointmentRequestsAsync();
            foreach (var i in requests)
            {
                mainStackPanel.Children.Add(await globals.CreateAppointmentRequestUI(i));
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }
    }
}
