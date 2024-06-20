using Hl7.Fhir.Model;
using Patient = Hl7.Fhir.Model.Patient;
using DiagnosticReport= Hl7.Fhir.Model.DiagnosticReport;
using myPatient= NeuroSpec.Shared.Models.DTO.Patient;
using Attachment=Hl7.Fhir.Model.Attachment;
using NeuroSpec.Shared.Models.DTO;
using System.Text;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
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
        public async static Task<MedicationRequest> ToHl7MedicationRequest(Prescription prescription,List<IssueDrug> issueDrugs)
        {
            var hl7MedicationRequest = new MedicationRequest();
            hl7MedicationRequest.Status = MedicationRequest.MedicationrequestStatus.Active;
            hl7MedicationRequest.Subject = new ResourceReference
            {
                Reference = $"Patient/{prescription.PatientID}"
            };
            hl7MedicationRequest.Recorder = new ResourceReference
            {
                Reference = $"Practitioner/{prescription.DoctorID}"
            };
            hl7MedicationRequest.Encounter = new ResourceReference
            {
                Reference = $"Appointment/{prescription.VisitID}"
            };

            hl7MedicationRequest.AuthoredOn = prescription.TimeStamp.ToLongDateString();
            IssueDrugService issueDrugService = new IssueDrugService();
            List<IssueDrug> issuedDrugs = await issueDrugService.GetAllIssueDrugsByPrescriptionIDAsync(prescription.PrescriptionID);
            List<Coding> issuedDrugsCoding = new List<Coding>();

            foreach (var issueDrug in issuedDrugs)
            {
                issuedDrugsCoding.Add(new Coding
                {
                    System = "http://hlsemops.microsoft.com",
                    Code = issueDrug.Name
                }) ;
            }
            CodeableConcept codeableConcept = new CodeableConcept
            {
                Coding = issuedDrugsCoding
            };

            hl7MedicationRequest.Medication = new CodeableReference
            {
                Concept = codeableConcept
            }; ;
            List<Annotation> notes = new List<Annotation>();
            foreach (var issueDrug in issueDrugs)
            {
                notes.Add(new Annotation
                {
                    Text = issueDrug.Notes
                });
            }
            hl7MedicationRequest.Note = notes;
            return hl7MedicationRequest;
        }
        
    }
}
