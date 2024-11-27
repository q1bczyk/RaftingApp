namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IBaseCrudService<TGetDTO, TCreateDTO> 
    where TGetDTO : class 
    where TCreateDTO : class
    {
        Task<TGetDTO> GetById(string id);
        Task<IEnumerable<TGetDTO>> GetAll();
        Task<TGetDTO> Create(TCreateDTO createDTO);
        Task<TGetDTO> Update(TCreateDTO updateDTO, string id);
    }
}