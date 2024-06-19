using clinical.Pages;
using MahApps.Metro.IconPacks;
using NeuroSpec.Shared.Models.DTO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User loggedIn;
        int state;

        public MainWindow(int stat, User user)
        {
            loggedIn = user;
            state = stat;
            InitializeComponent();
            globals.makeItLookClickableReverseColors(homeBTN);
            globals.makeItLookClickableReverseColors(patientSearchBTN);
            globals.makeItLookClickableReverseColors(calendarBtn);
            //globals.makeItLookClickableReverseColors(chatBtn);
            //globals.makeItLookClickableReverseColors(settingsBtn);

            homeBTN.Focus();
            //settingsBtn.Visibility = Visibility.Hidden;
            which();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        void whichTab(int curr)
        {
            homeBTN.BorderBrush = new SolidColorBrush(Colors.Transparent);
            patientSearchBTN.BorderBrush = new SolidColorBrush(Colors.Transparent);
            calendarBtn.BorderBrush = new SolidColorBrush(Colors.Transparent);
            //chatBtn.BorderBrush = new SolidColorBrush(Colors.Transparent);
            //settingsBtn.BorderBrush = new SolidColorBrush(Colors.Transparent);
            ((PackIconMaterial)homeBTN.Child).Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            ((PackIconMaterial)patientSearchBTN.Child).Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            ((PackIconMaterial)calendarBtn.Child).Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            //((PackIconMaterial)chatBtn.Child).Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            //((PackIconMaterial)settingsBtn.Child).Foreground = (Brush)Application.Current.Resources["lightFontColor"];

            if (curr == 0)
            {
                homeBTN.BorderBrush = (Brush)Application.Current.Resources["darkerColor"];
                ((PackIconMaterial)homeBTN.Child).Foreground = (Brush)Application.Current.Resources["darkerColor"];

            }
            else if (curr == 1)
            {
                patientSearchBTN.BorderBrush = (Brush)Application.Current.Resources["darkerColor"];
                ((PackIconMaterial)patientSearchBTN.Child).Foreground = (Brush)Application.Current.Resources["darkerColor"];


            }
            else if (curr == 2)
            {
                ((PackIconMaterial)calendarBtn.Child).Foreground = (Brush)Application.Current.Resources["darkerColor"];

                calendarBtn.BorderBrush = (Brush)Application.Current.Resources["darkerColor"];
            }
            //else if (curr == 3)
            //{
            //    ((PackIconMaterial)chatBtn.Child).Foreground = (Brush)Application.Current.Resources["darkerColor"];

            //    chatBtn.BorderBrush = (Brush)Application.Current.Resources["darkerColor"];
            //}
            //else if (curr == 4)
            //{
            //    ((PackIconMaterial)settingsBtn.Child).Foreground = (Brush)Application.Current.Resources["darkerColor"];

            //    settingsBtn.BorderBrush = (Brush)Application.Current.Resources["darkerColor"];
            //}
        }
        void which()
        {
            if (state == 1)
            {
                mainFrame.Navigate(new adminDashboardPage(loggedIn));
                //settingsBtn.Visibility = Visibility.Visible;
            }

            else if (state == 2)
            {
                mainFrame.Navigate(new DoctorDashboard(loggedIn));
                //new SideBarWindow().Show();

            }
            else mainFrame.Navigate(new ReceptionistDashboard(loggedIn));
        }
        private void homeBTN_Click(object sender, MouseButtonEventArgs e)
        {
            whichTab(0);
            which();
        }

        private void patientSearchBTN_Click(object sender, MouseButtonEventArgs e)
        {
            whichTab(1);

            mainFrame.Navigate(new PatientSearchPage());

        }

        private void calendarBtn_Click(object sender, MouseButtonEventArgs e)
        {
            whichTab(2);

            mainFrame.Navigate(new CalendarPage());

        }
        private void chatBtn_Click(object sender, MouseButtonEventArgs e)
        {
            whichTab(3);

            //mainFrame.Navigate(new ChatPage(loggedIn));

        }
        private void settingsBtn_Click(object sender, MouseButtonEventArgs e)
        {
            whichTab(4);
            //mainFrame.Navigate(new adminSettingsPage());

        }


        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1250;
                    this.Height = 830;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
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



        private void signOut(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to sign out?", "Sign Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                new loginPage().Show();
                Window.GetWindow(this).Close();
                globals.signedIn = null;
            }


        }

    }
}
