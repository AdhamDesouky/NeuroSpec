using clinical.BaseClasses;
using clinical.Pages;
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

namespace clinical
{
    /// <summary>
    /// Interaction logic for viewUser.xaml
    /// </summary>
    public partial class viewUser : Window
    {
        public viewUser(User user)
        {
            InitializeComponent();
            if (globals.signedIn.UserID.ToString().StartsWith('1'))
            {
                mainFrame.Navigate(new viewDoctor(user));
            }
        }

        private void closeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void minimizeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }
    }
}
