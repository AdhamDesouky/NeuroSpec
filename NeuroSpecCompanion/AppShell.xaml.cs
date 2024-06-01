using NeuroSpecCompanion.Views.MedicalHistory;

namespace NeuroSpecCompanion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ViewMedicalRecord), typeof(ViewMedicalRecord));

        }
    }
}
