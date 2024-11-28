using api;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Project.Core.Helpers;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.OtherServices
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient _filesContainer;
        private readonly AzureBlobConfig _blobConfig;
        public FileService(IOptions<AzureBlobConfig> config){
            _blobConfig = config.Value;
            string blobConnection = $"DefaultEndpointsProtocol=https;AccountName={config.Value.AccountName};AccountKey={config.Value.Key};EndpointSuffix=core.windows.net";
            _filesContainer = new BlobContainerClient(blobConnection, config.Value.ContainerName);
        }
        public async Task Delete(string imgUrl)
        {
            var blobClient = new BlobClient(new Uri(imgUrl), new StorageSharedKeyCredential(_blobConfig.AccountName, _blobConfig.Key));
            await blobClient.DeleteIfExistsAsync();
        }

        public Task<string> GetString(string imgUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            IsFileExtensionAllowed(file);
            fileName = fileName.ToLower().Replace(" ", "_"); 
            Stream stream = file.OpenReadStream();
            await _filesContainer.UploadBlobAsync(fileName, stream);
            string fileUrl = _filesContainer.GetBlobClient(fileName).Uri.ToString();
            return fileUrl;
        }

        private void IsFileExtensionAllowed(IFormFile file)
        {
            if(file == null) new ApiControlledException("Plik nie został przesłany", 400, "Plik jest wymagany");
            string[] allowExtensions = { ".jpg", ".jpeg", ".png"};
            string extension = Path.GetExtension(file.FileName).ToLower();
            if(!allowExtensions.Contains(extension)) throw new ApiControlledException("Format zdjęcia jest niepoprawnt", 400, "Dostępne formaty to jpg, jpeg, png");
        }
    }
}