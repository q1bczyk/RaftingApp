using Microsoft.AspNetCore.Http;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file, string fileName);
        Task Delete(string imgUrl);
        Task<string> GetString(string imgUrl);
    }
}