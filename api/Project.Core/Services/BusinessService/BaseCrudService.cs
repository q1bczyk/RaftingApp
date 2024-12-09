using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IMapper;
using Project.Core.Entities;

namespace Project.Core.Services.BusinessService
{
    public class BaseCrudService<TGetDTO, TCreateDTO, TUpdateDTO, TModel, TRepository> : IBaseCrudService<TGetDTO, TCreateDTO, TUpdateDTO>
    where TGetDTO : class
    where TCreateDTO : class
    where TUpdateDTO : class
    where TModel : BaseEntity
    where TRepository : IBaseRepository<TModel>
    {
        protected readonly TRepository _repository;
        protected readonly IBaseMapper<TCreateDTO, TModel> _toModelMapper;
        protected readonly IBaseMapper<TModel, TGetDTO> _toDTOMapper;
        public BaseCrudService(TRepository repository, IBaseMapper<TCreateDTO, TModel> toModelMapper, IBaseMapper<TModel, TGetDTO> toDTOMapper){
            _repository = repository;
            _toModelMapper = toModelMapper;
            _toDTOMapper = toDTOMapper;
        }
        public virtual async Task<TGetDTO> Create(TCreateDTO createDTO)
        {
            var dataToAdd = _toModelMapper.MapToModel(createDTO);
            var createdData = await _repository.Create(dataToAdd);
            return _toDTOMapper.MapToModel(createdData);
        }

        public virtual async Task Delete(string id)
        {
            var dataToDelete = await _repository.GetById(id);
            await _repository.Delete(dataToDelete);
        }

        public virtual async Task<List<TGetDTO>> GetAll()
        {
            var fetchedData = await _repository.GetAllAsync();
            return _toDTOMapper.MapToList(fetchedData);
        }

        public virtual async Task<TGetDTO> GetById(string id)
        {
            var fetchedData = await _repository.GetById(id);
            return _toDTOMapper.MapToModel(fetchedData);
        }

        public virtual async Task<TGetDTO> Update(TCreateDTO updateDTO, string id)
        {
            var dataToUpdate = await _repository.GetById(id);
            _toModelMapper.UpdateModel(updateDTO, dataToUpdate); 
            var updatedData = await _repository.Update(dataToUpdate);
            return _toDTOMapper.MapToModel(updatedData);
        }
    }
}