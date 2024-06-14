using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
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

        override public string ToString()
        {
            return UserID.ToString() + " " + FirstName;
        }


    }
}
