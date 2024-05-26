using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Referred { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public bool PreviouslyTreated { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double DueAmount { get; set; }
        public string referringName { get; set; }
        public string referringPN { get; set; }
        public int Age { get {
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - Birthdate.Year;
                if (currentDate.Month < Birthdate.Month || (currentDate.Month == Birthdate.Month && currentDate.Day < Birthdate.Day))
                {
                    age--;
                }
                return age;
            }
        }
        public string FullName { get { return FirstName + " " + LastName; } }

        public Patient(int patientID, string firstName, string lastname, 
            string password,
            DateTime birthdate, string gender, string phoneNumber, 
            string email, string address, 
            int DoctorId,
            bool referred, bool previouslyTreated, double height, 
            double weight, double dueAmount, string refN,string refPN)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastname;
            Password = password;
            Birthdate = birthdate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            DoctorID = DoctorId;
            Referred = referred;
            PreviouslyTreated = previouslyTreated;
            Height = height;
            Weight = weight;
            DueAmount = dueAmount;
            referringName = refN;
            referringPN=refPN;
        }

        // calculate age based on birthdate
        
        
    }
}
