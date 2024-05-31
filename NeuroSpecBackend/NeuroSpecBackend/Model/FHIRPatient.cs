using Hl7.Fhir.Model;
using NeuroSpec.Shared.Models.DTO;
using Patient = Hl7.Fhir.Model.Patient;
using myPatient= NeuroSpec.Shared.Models.DTO.Patient;
namespace NeuroSpecBackend.Model
{
    public static class FHIRPatient
    {
        public static Patient ToHl7Patient(myPatient patient)
        {
            var Patient = new Patient();
            var PatientName = new HumanName();
            PatientName.Use = HumanName.NameUse.Usual;
            var namelist = new[] { patient.FirstName };
            PatientName.Given = namelist;
            PatientName.Family = patient.LastName;
            Patient.Name.Add(PatientName);
            var PatientIdentifier = new Identifier();
            PatientIdentifier.System = "http://hlsemops.microsoft.com";
            PatientIdentifier.Value = patient.PatientID.ToString();
            Patient.Active = true;
            Patient.BirthDate = patient.DateOfBirth.ToString("yyyy-MM-dd");
            Patient.Identifier = new List<Identifier>
            {
                PatientIdentifier
            };
            Patient.Id = patient.PatientID.ToString();
            return Patient;
        }
    }
}
