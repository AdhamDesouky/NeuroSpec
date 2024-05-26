using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Services;
using System.Text.Json;

namespace NeuroSpecCompanion.Views.RegisterPages;

public partial class RegisterPage : ContentPage
{
    bool PasswordsMatch = false;
    bool dataVerified = false;
	public RegisterPage()
	{
		InitializeComponent();
        maleRB.IsChecked = true;
        rightHandRB.IsChecked = true;
        bddp.Date = new DateTime(2002,4,23);

	}

    private void OnUploadPhotoClicked(object sender, EventArgs e)
    {

    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        VerifyData();
        if (!dataVerified)
        {
            return;
        }
        //Register Patient
        Patient patient = new Patient();
        patient.Name.Add(new HumanName { Text = firstNameEntry.Text, Family = lastNameEntry.Text });
        patient.Address.Add(new Address { City = cityEntry.Text, Country = countryEntry.Text, PostalCode = zipEntry.Text,State=stateEntry.Text});
        patient.BirthDate = bddp.Date;
        patient.Gender = maleRB.IsChecked ? "Male" : "Female";
        patient.Telecom.Add(new ContactPoint { Value = phoneEntry.Text, System="Phone" }) ;
        patient.Telecom.Add(new ContactPoint { Value = emailEntry.Text, System = "Email" });

        patient.Password = passwordEntry.Text;
        patient.HeightInCm=(int)(HeightStepper.Value);
        patient.WeightInKg=(int)(WeightStepper.Value);

        var json = JsonSerializer.Serialize(patient);
        await DisplayAlert("Patient", json, "OK");
        PatientService _patientService = new PatientService();
        await _patientService.InsertPatientAsync(patient);
        await DisplayAlert("Success", "Patient registered successfully", "OK");

    }
    private bool VerifyPassword()
    {
        return (confirmPasswordEntry.Text.Trim() == passwordEntry.Text.Trim());
    }
    private void VerifyData()
    {
        dataVerified = false;
        if (string.IsNullOrEmpty(firstNameEntry.Text) || string.IsNullOrEmpty(lastNameEntry.Text) || string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text) || string.IsNullOrEmpty(confirmPasswordEntry.Text) || string.IsNullOrEmpty(phoneEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all the fields", "OK");
            return;
        }
        if (!PasswordsMatch)
        {
            DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }
        if (!string.IsNullOrEmpty(emailEntry.Text)&&(!emailEntry.Text.Contains('@') || !emailEntry.Text.Contains('.')))
        {
            DisplayAlert("Error", "Invalid email address", "OK");
            return;
        }
        if (phoneEntry.Text.Length != 11 &&phoneEntry.Text.Length!=13)
        {
            DisplayAlert("Error", "Invalid phone number", "OK");
            return;
        }
        if (WeightStepper.Value == 0)
        {
            DisplayAlert("Error", "Invalid weight", "OK");
            return;
        }
        if (WeightStepper.Value == 0)
        {
            DisplayAlert("Error", "Invalid height", "OK");
            return;
        }
        if(bddp.Date.Year > DateTime.Now.Year - 18)
        {
            DisplayAlert("Error", "You must be at least 18 years old to register", "OK");
            return;
        }
        if(firstNameEntry.Text.Length < 3 || lastNameEntry.Text.Length < 3)
        {
            DisplayAlert("Error", "First and last names must be at least 3 characters long", "OK");
            return;
        }
        if(passwordEntry.Text.Length < 6)
        {
            DisplayAlert("Error", "Password must be at least 6 characters long", "OK");
            return;
        }
        dataVerified = true;

    }
    private void OnWeightStepperChange(object sender, ValueChangedEventArgs e)
    {
        int weight = (int)e.NewValue;
        weightlbl.Text = weight.ToString()+" kg.";
    }

    private void OnHeightStepperChange(object sender, ValueChangedEventArgs e)
    {
        int height = (int)e.NewValue;
        heightlbl.Text = height.ToString()+" c.m.";
    }

    private void VerifyPassword(object sender, TextChangedEventArgs e)
    {
        if (!VerifyPassword())
        {
            passwordValidationlbl.Text = "Passwords do not match";
            PasswordsMatch = false;
            return;
        };
        passwordValidationlbl.Text = "";
        PasswordsMatch = true;
    }
}