using Project.Core.Entities;
using Project.Core.Interfaces.IRepositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class EquipmentTypeRepository : BaseRepository<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(DataContext context) : base(context){}
    }
}