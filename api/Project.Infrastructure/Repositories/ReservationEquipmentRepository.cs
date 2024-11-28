using Project.Core.Entities;
using Project.Core.Interfaces.IRepositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class ReservationEquipmentRepository : BaseRepository<ReservationEquipment>, IReservationEquipmentRepository
    {
        public ReservationEquipmentRepository(DataContext context) : base(context)
        {
            
        }
    }
}