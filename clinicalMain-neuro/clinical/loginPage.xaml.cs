using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace clinical
{

    public partial class loginPage : Window
    {
        UserService userService = new UserService();
        AttendanceRecordService attendanceRecordService = new AttendanceRecordService();
        public loginPage()
        {
            //new globals();
            InitializeComponent();
            
            //new DB();
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

        async void login()
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


            User user = await userService.GetUserByIdAsync(id);
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
            await attendanceRecordService.InsertAttendanceRecordAsync(new AttendanceRecord
            {
                RecordID = IDGeneration.generateNewAttendanceRecordID(user.UserID),
                TimeStamp = DateTime.Now,
                UserID = user.UserID,
                IsPresent = true
            });
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
