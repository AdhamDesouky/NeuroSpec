using Firebase.Storage;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Views
{
    public partial class UserProfilePage : ContentPage
    {
        private Stream _fileStream;

        public UserProfilePage()
        {
            InitializeComponent();
        }

        private async void OnUploadPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select a photo"
                });

                if (result != null)
                {
                    _fileStream = await result.OpenReadAsync();

                    // Display the selected photo
                    userImageButton.Source = ImageSource.FromStream(() => _fileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
        {
            try
            {
                var fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a file"
                });

                if (fileResult != null)
                {
                    _fileStream = await fileResult.OpenReadAsync();
                    statusLabel.Text = "Uploading...";

                    // Upload the file to Firebase Storage
                    var firebaseStorage = new FirebaseStorage("neurospec-d06c2.appspot.com");
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileResult.FileName)}";
                    var storageReference = firebaseStorage
                        .Child("uploads")
                        .Child(fileName);

                    var downloadUrl = await storageReference.PutAsync(_fileStream);
                    statusLabel.Text = "Upload successful!";
                    Console.WriteLine($"File URL: {downloadUrl}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                statusLabel.Text = "Failed to upload file.";
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
