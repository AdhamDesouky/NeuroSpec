
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace NeuroSpec.Shared.Models.DTO
{
    /// <summary>
    /// References Patient in FHIR
    /// </summary>
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int PatientID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// True for male, false for female
        /// </summary>
        public bool Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        /// <summary>
        /// True for right hand, false for left hand
        /// </summary>
        public bool DominantHand { get; set; }
    }


    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}