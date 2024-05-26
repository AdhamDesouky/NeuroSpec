using clinical.BaseClasses;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newEmployeePage.xaml
    /// </summary>
    public partial class newEmployeePage : Page
    {
        List<int> selectedDays = new List<int>();
        bool IsDoctor;
        public newEmployeePage(bool isDoctor)
        {
            InitializeComponent();

            IsDoctor = isDoctor;
            bdDatePicker.SelectedDate = DateTime.Now;
            hiringDatePicker.SelectedDate = DateTime.Now;
            sundayCB.IsChecked = true;
            mondayCB.IsChecked = true;
            tuesdayCB.IsChecked = true;
            wednesdayCB.IsChecked = true;
            thursdayCB.IsChecked = true;
            selectedDays.Add(2);
            selectedDays.Add(3);
            selectedDays.Add(4);
            selectedDays.Add(5);
            selectedDays.Add(6);
        }
        private void save(object sender, MouseButtonEventArgs e)
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
            if (DB.GetUserByNID(nid) != null)
            {
                MessageBox.Show("National ID already exists", "Error");
                return;
            }
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
                id = globals.generateNewDoctorID(nid);
            }
            else id = globals.generateNewEmployeeID(nid);

            User user = new User(id, fn.Trim(), ln.Trim(), gender.Trim(), hd, bd, address.Trim(), phone.Trim(), email.Trim(), nid.Trim(), password);
            DB.InsertUser(user);
            selectedDays = selectedDays.Distinct().ToList();
            DB.updateUserWorkDays(user.UserID, selectedDays);


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

        private void selectDay(object sender, RoutedEventArgs e)
        {

            if (((CheckBox)sender).Content == "Saturday")
            {
                selectedDays.Add(1);
            }
            else if (((CheckBox)sender).Content == "Sunday")
            {
                selectedDays.Add(2);
            }
            else if (((CheckBox)sender).Content == "Monday")
            {
                selectedDays.Add(3);
            }
            else if (((CheckBox)sender).Content == "Tuesday")
            {
                selectedDays.Add(4);
            }
            else if (((CheckBox)sender).Content == "Wednesday")
            {
                selectedDays.Add(5);
            }
            else if (((CheckBox)sender).Content == "Thursday")
            {
                selectedDays.Add(6);
            }
            else if (((CheckBox)sender).Content == "Friday")
            {
                selectedDays.Add(7);
            }
        }

        private void unSelectDay(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).Content == "Saturday")
            {
                selectedDays.Remove(1);
            }
            else if (((CheckBox)sender).Content == "Sunday")
            {
                selectedDays.Remove(2);
            }
            else if (((CheckBox)sender).Content == "Monday")
            {
                selectedDays.Remove(3);
            }
            else if (((CheckBox)sender).Content == "Tuesday")
            {
                selectedDays.Remove(4);
            }
            else if (((CheckBox)sender).Content == "Wednesday")
            {
                selectedDays.Remove(5);
            }
            else if (((CheckBox)sender).Content == "Thursday")
            {
                selectedDays.Remove(6);
            }
            else if (((CheckBox)sender).Content == "Friday")
            {
                selectedDays.Remove(7);
            }

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
