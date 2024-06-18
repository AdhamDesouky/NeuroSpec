using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newEmployeePage.xaml
    /// </summary>
    public partial class newEmployeePage : Page
    {
        bool IsDoctor;
        UserService userService = new UserService();

        public newEmployeePage(bool isDoctor)
        {
            InitializeComponent();

            IsDoctor = isDoctor;
            bdDatePicker.SelectedDate = DateTime.Now;
            hiringDatePicker.SelectedDate = DateTime.Now;
        }
        private async void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln = lastNameTextBox.Text;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address = addressTextBox.Text;
            string phone = phoneTextBox.Text;
            DateTime bd = DateTime.Now;
            if (bdDatePicker.SelectedDate.HasValue)
                bd = bdDatePicker.SelectedDate.Value;

            DateTime hd = DateTime.Now;
            if (hiringDatePicker.SelectedDate.HasValue)
                hd = hiringDatePicker.SelectedDate.Value;

            string nid = NIDTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordBox.Password;


            if (fn == "" || ln == "" || address == "" || phone == "" || nid == "" || email == "")
            {
                MessageBox.Show("Please fill all the fields", "Error");
                return;
            }
            if (nid.Length != 14)
            {
                MessageBox.Show("National ID must be 14 digits", "Error");
                return;
            }
            if (phone.Length != 11)
            {
                MessageBox.Show("Phone Number must be 11 digits", "Error");
                return;
            }
            if (email.Length < 5 || !email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Invalid Email", "Error");
                return;
            }
            //if (userService.GetUserByNIDAsync(nid) != null)
            //{
            //    MessageBox.Show("National ID already exists", "Error");
            //    return;
            //}
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters", "Error");
                return;
            }
            if (password != passwordConfirmBox.Password)
            {
                MessageBox.Show("Passwords don't match", "Error");
                return;
            }
            int id;

            if (IsDoctor)
            {
                id = IDGeneration.generateNewDoctorID(nid);
            }
            else id = IDGeneration.generateNewEmployeeID(nid);

            User user = new User
            {
                UserID = id,
                FirstName = fn.Trim(),
                LastName = ln.Trim(),
                Email = email.Trim(),
                Gender = gender.Trim() == "Male",
                Address = address.Trim(),
                PhoneNumber = phone.Trim(),
                NationalID = nid.Trim(),
                Password = password,
                HireDate = hd,
                Birthdate = bd
            };
            await userService.InsertUserAsync(user);


            MessageBox.Show("Successfully added new User, ID: " + user.UserID, "Success");

            Window.GetWindow(this).Close();
        }

        private void nowCheck(object sender, RoutedEventArgs e)
        {
            hiringDatePicker.SelectedDate = DateTime.Now;
            hiringDatePicker.IsEnabled = false;
        }

        private void nowUnCheck(object sender, RoutedEventArgs e)
        {
            hiringDatePicker.IsEnabled = true;

        }


        private void TogglePassword(object sender, MouseButtonEventArgs e)
        {


            if (passwordBox.Visibility == Visibility.Hidden)
            {
                passwordConfirmBox.Password = passwordConfirmTB.Text;
                passwordBox.Password = passwordTB.Text;
                passwordBox.Visibility = Visibility.Visible;
                passwordConfirmBox.Visibility = Visibility.Visible;
                passwordTB.Visibility = Visibility.Hidden;
                passwordConfirmTB.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordTB.Text = passwordBox.Password;
                passwordConfirmTB.Text = passwordConfirmBox.Password;
                passwordBox.Visibility = Visibility.Hidden;
                passwordConfirmBox.Visibility = Visibility.Hidden;
                passwordTB.Visibility = Visibility.Visible;
                passwordConfirmTB.Visibility = Visibility.Visible;
            }
        }

        private void mathcingPassword(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == passwordConfirmBox.Password && passwordBox.Password.Length != 0)
            {
                correctPassIcon.Visibility = Visibility.Visible;
                wrongPassIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                correctPassIcon.Visibility = Visibility.Hidden;
                wrongPassIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
