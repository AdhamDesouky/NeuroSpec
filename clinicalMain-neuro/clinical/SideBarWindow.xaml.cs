using clinical.Pages;
using System.Windows;
using System.Windows.Input;

namespace clinical
{
    /// <summary>
    /// Interaction logic for SideBarWindow.xaml
    /// </summary>
    public partial class SideBarWindow : Window
    {
        public SideBarWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            mainFrame.Navigate(new DoctorSideBar());

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            Left = screenWidth - Width;
            Top = screenHeight / 2 - Height / 2;

        }

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Width = MaxWidth;
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Width= MinWidth;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
