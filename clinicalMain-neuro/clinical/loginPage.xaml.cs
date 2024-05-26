using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

    public partial class loginPage : Window
    {
        public loginPage()
        {
            new globals();
            InitializeComponent();
            new DB();
            txtEmail.Focus();
            //ontology oi = new ontology();
            //foreach(var i in oi.GetAllOntologies())
            //{
            //    DB.InsertTerm(i);
            //}
            
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmailHint.Visibility = Visibility.Collapsed;
            else
                textEmailHint.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void logInBTN_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { login(); }
        }

        void login()
        {
            if (passwordTB.Password.Length == 0 || txtEmail.Text.Length == 0)
            {
                MessageBox.Show("Please fill all the fields", "Error");
                return;
            }
            int id;
            try
            {
                id = Convert.ToInt32(txtEmail.Text);
            }
            catch
            {
                MessageBox.Show("Please enter a valid ID", "Error");
                return;
            }


            User user = DB.GetUserById(id);
            if (user != null)
            {
                string password = passwordTB.Password;
                if (password != user.Password)
                {
                    MessageBox.Show("Wrong Password");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Invalid ID");
                return;
            }

            MessageBox.Show("Welcome, " + (user.UserID.ToString()[0] != '3' ? "Dr. " : "") + user.FirstName);
            int s;
            if (txtEmail.Text.ToString().StartsWith("1")) s = 1;
            else if (txtEmail.Text.ToString().StartsWith("2")) s = 2;
            else s = 3;
            globals.signedIn = user;

            MainWindow mainWindow = new MainWindow(s, user);
            DB.InsertAttendanceRecord(new AttendanceRecord(globals.generateNewAttendanceRecordID(user.UserID), DateTime.Now, user.UserID, true));
            mainWindow.Show();
            this.Close();
        }

        private void passwordHint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordTB.Focus();

        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordTB.Password) && passwordTB.Password.Length > 0)
                passwordHint.Visibility = Visibility.Collapsed;
            else
                passwordHint.Visibility = Visibility.Visible;
        }
    }

}
