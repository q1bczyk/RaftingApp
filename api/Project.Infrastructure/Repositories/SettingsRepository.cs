using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Core.Interfaces.IRepositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class SettingsRepository : BaseRepository<Settings>, ISettingsRepository
    {
        public SettingsRepository(DataContext context) : base(context)
        {
        }

        public async Task<Settings> GetSettingsAsync()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }
    }
}