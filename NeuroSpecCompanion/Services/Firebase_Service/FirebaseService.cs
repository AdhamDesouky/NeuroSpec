using Firebase.Storage;

namespace NeuroSpecCompanion.Services.FHIR_Base
{
    public class FirebaseService
    {
        private readonly FirebaseStorage _firebaseStorage;
        private readonly string _url;

        public FirebaseService()
        {
            _firebaseStorage = new FirebaseStorage(_url);
            _url = "neurospec-d06c2.appspot.com";

        }
        public async Task<string> UploadFile(Stream _fileStream, FileResult fileResult)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileResult.FileName)}";
            var storageReference = _firebaseStorage
            .Child("uploads")
            .Child(fileName);

            var downloadUrl = await storageReference.PutAsync(_fileStream);
            return downloadUrl;

        }
    }
}
