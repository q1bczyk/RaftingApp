using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Core.Interfaces.IRepositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DataContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Reservation>> GetAllAsync(){
            var reservations = await _context.Reservations
                .Include(r => r.ReservationEquipment)
                    .ThenInclude(re => re.EquipmentType)
                .ToListAsync();

            return reservations;
        }
    }
}