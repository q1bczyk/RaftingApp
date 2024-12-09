namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IBaseCrudService<TGetDTO, TCreateDTO, TUpdateDTO> 
    where TGetDTO : class 
    where TCreateDTO : class
    {
        Task<TGetDTO> GetById(string id);
        Task<List<TGetDTO>> GetAll();
        Task<TGetDTO> Create(TCreateDTO createDTO);
        Task<TGetDTO> Update(TCreateDTO updateDTO, string id);
        Task Delete(string id);
    }
}