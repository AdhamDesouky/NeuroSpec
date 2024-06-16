using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuroSpec.Shared.Services.Firebase_Service
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
        public async Task<string> UploadFile(Stream _fileStream)
        {
            var fileName = $"{Guid.NewGuid()}";
            var storageReference = _firebaseStorage
            .Child("uploads")
            .Child(fileName);

            var downloadUrl = await storageReference.PutAsync(_fileStream);
            return downloadUrl;

        }
        public async Task<string> UploadProfilePicture(Stream _fileStream)
        {
            var fileName = $"{Guid.NewGuid()}";
            var storageReference = _firebaseStorage
            .Child("profilePictures")
            .Child(fileName);

            var downloadUrl = await storageReference.PutAsync(_fileStream);
            return downloadUrl;

        }
    }
}
