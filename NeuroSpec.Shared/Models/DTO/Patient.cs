using System;
using System.Collections.Generic;

namespace NeuroSpec.Shared.Models.DTO
{
/// <summary>
/// Very unstable model, needs to be refactored
/// </summary>
    public class Patient
    {
        public string ResourceType { get; set; } = "Patient";
        public Identifier Identifier { get; set; }
        public string Password { get; set; }
        public List<HumanName> Name { get; set; }
        public List<ContactPoint> Telecom { get; set; }
        public string Gender { get; set; } // male | female | other | unknown
        public DateTime BirthDate { get; set; }

#nullable enable
        public List<Address>? Address { get; set; }
        public bool? DeceasedBoolean { get; set; }
        public DateTime? DeceasedDateTime { get; set; }
        public CodeableConcept? MaritalStatus { get; set; }
        public bool? MultipleBirthBoolean { get; set; }
        public int? MultipleBirthInteger { get; set; }
        public List<Attachment>? Photo { get; set; }
        public List<Contact>? Contact { get; set; }
        public List<Communication>? Communication { get; set; }
        public List<Reference>? GeneralPractitioner { get; set; }
        public Reference? ManagingOrganization { get; set; }
#nullable disable

        
        public int HeightInCm { get; set; }
        public int WeightInKg { get; set; }

        public List<Link> Link { get; set; }

        public Patient()
        {
            Identifier = new Identifier();
            Name = new List<HumanName>();
            Telecom = new List<ContactPoint>();
            Address = new List<Address>();
            Photo = new List<Attachment>();
            Contact = new List<Contact>();
            Communication = new List<Communication>();
            GeneralPractitioner = new List<Reference>();
            Link = new List<Link>();
        }
        
    }

    public class Identifier
    {
        public string System { get; set; }
        public string Value { get; set; }

        public Identifier() { }

        public Identifier(string system, string value)
        {
            System = system;
            Value = value;
        }
    }

    public class HumanName
    {
        public string? Use { get; set; } // usual | official | temp | nickname | anonymous | old | maiden
        public string Text { get; set; }
        public string Family { get; set; }
        public string? Given { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }

        public HumanName()
        {
        }
        public string FastToString()
        {
            return Prefix+" "+Text+" "+Family+" "+Suffix;
        }
    }

    public class ContactPoint
    {
        public string System { get; set; } // phone | fax | email | pager | url | sms | other
        public string Value { get; set; }
        public string? Use { get; set; } // home | work | temp | old | mobile
        public int? Rank { get; set; }

        public ContactPoint() { }
        public string FastToString()
        {
            return Use + " " + Value;
        }
    }
    public class Address
    {
        public string Use { get; set; } // home | work | temp | old | billing
        public string Type { get; set; } // postal | physical | both
        public string Text { get; set; }
        public List<string> Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address()
        {
            Line = new List<string>();
        }
        public string FastToString()
        {
            return Text + " " + City + " " + State + " " + PostalCode + " " + Country;
        }
    }

    public class CodeableConcept
    {
        public List<Coding> Coding { get; set; }
        public string Text { get; set; }

        public CodeableConcept()
        {
            Coding = new List<Coding>();
        }
    }

    public class Coding
    {
        public string System { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }

        public Coding() { }
    }

    public class Attachment
    {
        public string ContentType { get; set; }
        public string Language { get; set; }
        public string Data { get; set; }
        public string Url { get; set; }
        public int? Size { get; set; }
        public string Hash { get; set; }
        public string Title { get; set; }
        public DateTime? Creation { get; set; }

        public Attachment() { }
    }

    public class Contact
    {
        public List<CodeableConcept> Relationship { get; set; }
        public HumanName Name { get; set; }
        public List<ContactPoint> Telecom { get; set; }
        public Address Address { get; set; }
        public string Gender { get; set; } // male | female | other | unknown
        public Reference Organization { get; set; }
        public Period Period { get; set; }

        public Contact()
        {
            Relationship = new List<CodeableConcept>();
            Telecom = new List<ContactPoint>();
        }
    }

    public class Communication
    {
        public CodeableConcept Language { get; set; }
        public bool Preferred { get; set; }

        public Communication() { }
    }

    public class Reference
    {
        public string ReferenceId { get; set; }
        public string Type { get; set; }
        public string Display { get; set; }

        public Reference() { }
    }

    public class Link
    {
        public Reference Other { get; set; }
        public string Type { get; set; } // replaced-by | replaces | refer | seealso

        public Link() { }
    }

    public class Period
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public Period() { }
    }
}
