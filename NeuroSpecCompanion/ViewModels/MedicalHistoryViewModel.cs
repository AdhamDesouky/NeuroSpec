using Firebase.Storage;
using MvvmHelpers;
using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Services.OCR_Service;
using NeuroSpecCompanion.Services.PDF_OCR_Service;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Views.MedicalHistory;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NeuroSpecCompanion.ViewModels
{
    public class MedicalHistoryViewModel : BaseViewModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        public ObservableCollection<MedicalRecord> MedicalRecords { get; }

        public ICommand UploadFileCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ViewRecordCommand { get; }

        public MedicalHistoryViewModel()
        {
            _medicalRecordService = new MedicalRecordService();
            MedicalRecords = new ObservableCollection<MedicalRecord>();
            UploadFileCommand = new Command(OnUploadFileClicked);
            DeleteCommand = new Command<MedicalRecord>(OnDeleteFileClicked);
            ViewRecordCommand = new Command<MedicalRecord>(OnViewRecordClicked);

            LoadMedicalRecords();
        }

        private async void LoadMedicalRecords()
        {
            var records = await _medicalRecordService.GetAllPatientRecordsAsync(LoggedInPatientService.LoggedInPatient.PatientID);
            foreach (var record in records)
            {
                MedicalRecords.Add(record);
            }
        }

        private async void OnUploadFileClicked()
        {
            try
            {
                var fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an image or PDF",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "public.image", "com.adobe.pdf" } },
                        { DevicePlatform.Android, new[] { "image/*", "application/pdf" } }
                    })
                });

                if (fileResult != null)
                {
                    int patientId = LoggedInPatientService.LoggedInPatient.PatientID;
                    int recordId = IDGeneration.generateNewRecordID(patientId);
                    string downloadLink = await UploadFile(fileResult, recordId);

                    var extractedText = await ExtractTextFromFile(fileResult);
                    MedicalRecord medicalRecord = new MedicalRecord
                    {
                        RecordID = recordId,
                        Type = "LAB",
                        TimeStamp = DateTime.Now,
                        Report = extractedText,
                        VisualAttachments = new List<Attachment> {
                        new Attachment { url = downloadLink, title = fileResult.FileName, contentType = fileResult.ContentType }
                    },
                        PatientID = patientId,
                        DoctorNotes = ""
                    };

                    MedicalRecords.Add(medicalRecord);
                    await _medicalRecordService.InsertMedicalRecordAsync(medicalRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async Task<string> UploadFile(FileResult file, int recordId)
        {
            try
            {
                using var stream = await file.OpenReadAsync();
                var firebaseStorage = new FirebaseStorage("neurospec-d06c2.appspot.com")
                    .Child("uploads")
                    .Child($"{LoggedInPatientService.LoggedInPatient.PatientID}")
                    .Child("medicalRecords")
                    .Child($"{recordId}_{file.FileName}");

                var downloadUrl = await firebaseStorage.PutAsync(stream);

                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "";
            }
        }

        private async Task<string> ExtractTextFromFile(FileResult file)
        {
            try
            {
                using var stream = await file.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                if (file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var ocr = new PDFOCRService();
                    return await ocr.ReadTextFromPDFAsync(memoryStream);
                }
                else
                {
                    var ocr = new OCRService();
                    return await ocr.ReadTextFromImageAsync(memoryStream);
                }
            }
            catch (Exception ex)
            {
                return $"Failed to extract text: {ex.Message}";
            }
        }

        private async void OnDeleteFileClicked(MedicalRecord record)
        {
            try
            {
                var firebaseStorage = new FirebaseStorage("neurospec-d06c2.appspot.com")
                    .Child("uploads")
                    .Child($"{LoggedInPatientService.LoggedInPatient.PatientID}")
                    .Child("medicalRecords")
                    .Child($"{record.RecordID}");

                await firebaseStorage.DeleteAsync();

                MedicalRecords.Remove(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async void OnViewRecordClicked(MedicalRecord record)
        {
            await Shell.Current.GoToAsync($"{nameof(ViewMedicalRecord)}", new Dictionary<string, object>
    {
        { "Record", record }
    });
        }

    }

}
