namespace NeuroSpecCompanion.Views
{
    public partial class UserProfilePage : ContentPage
    {
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
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    var byteArray = memoryStream.ToArray();

                    // Use byteArray to set the source of the user image
                    // For example:
                    // userImage.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
            }
        }

       
    }
}
