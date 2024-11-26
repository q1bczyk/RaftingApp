using api;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Project.Core.Helpers;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.OtherServices
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient FilesContainer;
        public FileService(IOptions<AzureBlobConfig> config){
            string blobConnection = $"DefaultEndpointsProtocol=https;AccountName={config.Value.AccountName};AccountKey={config.Value.Key};EndpointSuffix=core.windows.net";
            FilesContainer = new BlobContainerClient(blobConnection, config.Value.ContainerName);
        }
        public Task Delete(string imgUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetString(string imgUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            IsFileExtensionAllowed(file);
            Stream stream = file.OpenReadStream();
            await FilesContainer.UploadBlobAsync(fileName, stream);
            string fileUrl = FilesContainer.GetBlobClient(fileName).Uri.ToString();
            return fileUrl;
        }

        private void IsFileExtensionAllowed(IFormFile file)
        {
            string[] allowExtensions = { ".jpg", ".jpeg", ".png"};
            string extension = Path.GetExtension(file.FileName).ToLower();
            if(!allowExtensions.Contains(extension)) throw new ApiControlledException("Format zdjęcia jest niepoprawnt", 400, "Dostępne formaty to jpg, jpeg, png");
        }
    }
}