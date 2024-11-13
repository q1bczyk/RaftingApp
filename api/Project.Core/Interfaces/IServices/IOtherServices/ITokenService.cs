using Project.Core.Entities;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}