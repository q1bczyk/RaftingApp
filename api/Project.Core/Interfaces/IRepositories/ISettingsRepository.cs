using Project.Core.Entities;

namespace Project.Core.Interfaces.IRepositories
{
    public interface ISettingsRepository : IBaseRepository<Settings>
    {
        Task<Settings> GetSettingsAsync();
    }
}