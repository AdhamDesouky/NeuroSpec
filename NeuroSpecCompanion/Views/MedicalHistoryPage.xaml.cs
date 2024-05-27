using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Firebase.Storage;
using IronOcr;
using System.Linq;

namespace NeuroSpecCompanion.Views
{
    public partial class MedicalHistoryPage : ContentPage
    {
        private ObservableCollection<FileMetadata> UploadedFiles { get; set; }

        public MedicalHistoryPage()
        {
            InitializeComponent();
            UploadedFiles = new ObservableCollection<FileMetadata>
            {
                new FileMetadata
                {
                    FileName = "Demo.pdf",
                    FileUrl = "https://example.com/demo/DemoFile1.pdf"
                },
                new FileMetadata
                {
                    FileName = "ay file below is real data.pdf",
                    FileUrl = "https://example.com/demo/DemoFile5.pdf"
                }
            };
            HistoryCollectionView.ItemsSource = UploadedFiles;
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
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
                    await UploadFile(fileResult);

                    var extractedText = await ExtractTextFromFile(fileResult);
                    ExtractedTextEditor.Text = extractedText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async Task UploadFile(FileResult file)
        {
            try
            {
                using var stream = await file.OpenReadAsync();
                var firebaseStorage = new FirebaseStorage("neurospec-d06c2.appspot.com")
                    .Child("uploads")
                    .Child(file.FileName);

                var downloadUrl = await firebaseStorage.PutAsync(stream);

                // Add file metadata to the collection
                UploadedFiles.Add(new FileMetadata
                {
                    FileName = file.FileName,
                    FileUrl = downloadUrl
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async Task<string> ExtractTextFromFile(FileResult file)
        {
            var ocr = new IronTesseract();
            try
            {
                using var stream = await file.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                if (file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    using var input = new OcrInput(memoryStream);
                    var result = ocr.Read(input);
                    return result.Text;
                }
                else
                {
                    using var input = new OcrInput(memoryStream.ToArray());
                    var result = ocr.Read(input);
                    return result.Text;
                }
            }
            catch (Exception ex)
            {
                return $"Failed to extract text: {ex.Message}";
            }
        }

        private async void OnDeleteFileClicked(object sender, EventArgs e)
        {
            var menuItem = sender as SwipeItem;
            var fileMetadata = menuItem.CommandParameter as FileMetadata;

            try
            {
                var firebaseStorage = new FirebaseStorage("neurospec-d06c2.appspot.com")
                    .Child("uploads")
                    .Child(fileMetadata.FileName);

                await firebaseStorage.DeleteAsync();

                UploadedFiles.Remove(fileMetadata);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    public class FileMetadata
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
