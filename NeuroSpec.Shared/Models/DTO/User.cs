using System;

namespace NeuroSpec.Shared.Models.DTO
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalID { get; set; }
        public string Password { get; set; }
        public string FullName { get { return FirstName + " " + LastName;}}
        public bool isReciptionist { get { return UserID.ToString().StartsWith('3'); } }
        public bool isEmployee { get { return UserID.ToString().StartsWith('2'); } }
        public bool isAdmin { get { return UserID.ToString().StartsWith('1'); } }

        public User(int userID, string firstName, string lastName, bool gender, DateTime hireDate, DateTime birthdate, string address, string phoneNumber, string email, string nationalID, string password)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            HireDate = hireDate;
            Birthdate = birthdate;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            NationalID = nationalID;
            Password = password;
        }
        public User()
        {
        }   
        override public string ToString()
        {
            return UserID.ToString() + " " + FirstName;
        }


    }
}
