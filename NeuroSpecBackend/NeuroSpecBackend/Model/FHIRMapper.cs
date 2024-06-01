using Hl7.Fhir.Model;
using Patient = Hl7.Fhir.Model.Patient;
using DiagnosticReport= Hl7.Fhir.Model.DiagnosticReport;
using myPatient= NeuroSpec.Shared.Models.DTO.Patient;
using Attachment=Hl7.Fhir.Model.Attachment;
using NeuroSpec.Shared.Models.DTO;
using System.Text;
namespace NeuroSpecBackend.Model
{
    public static class FHIRMapper
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
        public static DiagnosticReport ToHl7MedicalRecord(MedicalRecord medicalRecord)
        {
            var diagnosticReport = new DiagnosticReport();
            diagnosticReport.Status = DiagnosticReport.DiagnosticReportStatus.Final;
            diagnosticReport.Identifier = new List<Identifier>
            {
                new Identifier
                {
                    System = "http://hlsemops.microsoft.com",
                    Value = medicalRecord.RecordID.ToString()
                }
            };
            diagnosticReport.Subject = new ResourceReference
            {
                Reference = $"Patient/{medicalRecord.PatientID}"
            };
            diagnosticReport.Category = new List<CodeableConcept>
            {
                new CodeableConcept
                {
                    Coding = new List<Coding>
                    {
                        new Coding
                        {
                            System = "http://hlsemops.microsoft.com",
                            Code = medicalRecord.Type
                        }
                    }
                }
            };
            diagnosticReport.Effective = new FhirDateTime(medicalRecord.TimeStamp);
            diagnosticReport.PresentedForm = new List<Attachment>
            {
                new Attachment
                {
                    ContentType = "text/plain",
                    Data = Encoding.UTF8.GetBytes(medicalRecord.Report)
                }
            };
            if (medicalRecord.VisualAttachments.Count > 0)
            {
                diagnosticReport.Media = new List<DiagnosticReport.MediaComponent>
                {
                    new DiagnosticReport.MediaComponent
                    {
                        Link = new ResourceReference
                        {
                            Reference = $"Media/{medicalRecord.VisualAttachments[0].url}",
                        }
                    }
                };
            }
            diagnosticReport.Note = new List<Annotation>
            {
                new Annotation
                {
                    Text = medicalRecord.DoctorNotes
                }
            };

            return diagnosticReport;
        }
    }
}
