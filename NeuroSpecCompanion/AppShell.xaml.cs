using NeuroSpecCompanion.Views.BookAppointment;
using NeuroSpecCompanion.Views.MedicalHistory;

namespace NeuroSpecCompanion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ViewMedicalRecord), typeof(ViewMedicalRecord));
            Routing.RegisterRoute(nameof(ViewAppointmentPage), typeof(ViewAppointmentPage));
            Routing.RegisterRoute(nameof(BookAppointmentMainPage), typeof(BookAppointmentMainPage));
            Routing.RegisterRoute(nameof(ViewAllAppointmentsPage), typeof(ViewAllAppointmentsPage));

        }
    }
}
