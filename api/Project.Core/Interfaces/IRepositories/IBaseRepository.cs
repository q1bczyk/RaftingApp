namespace Project.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetById<Tid>(Tid id);
        Task<T> Create(T model);
        Task CreateRange(List<T> model);
        Task<T> Update(T model);
        Task<T> Delete(T model);
        Task<bool> Save();
    }
}