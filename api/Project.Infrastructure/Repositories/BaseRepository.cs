using api;
using Microsoft.EntityFrameworkCore;
using Project.Core.Interfaces.IRepositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;
        protected DbSet<T> DbSet => _context.Set<T>();
        public async Task<T> Create(T model)
        {
            await _context.Set<T>().AddAsync(model);
            return model;
        }

        public async Task<T> Delete(T model)
        {
            _context.Set<T>().Remove(model);
            await Save();
            return model;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<Tid>(Tid id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            if(data == null)
                throw new NotFoundException("Not found!");
            return data;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Update(T model)
        {
            _context.Set<T>().Update(model);
            await Save();
            return model;
        }
    }
}