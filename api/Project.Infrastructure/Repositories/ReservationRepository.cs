using System.Text.Json;
using api;
using Microsoft.EntityFrameworkCore;
using Project.Core.DTO.ReservationsDTO;
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

        public async Task<List<Reservation>> GetFilteredData(
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? specificDate = null,
            string lastNamePartial = null,
            string reservationId = null
        )
        {
            var query = _context.Reservations
                .Include(r => r.Payment)
                .Include(r => r.ReservationEquipment)
                    .ThenInclude(re => re.EquipmentType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(reservationId))
                query = query.Where(r => r.Id.Contains(reservationId));

            if (!string.IsNullOrWhiteSpace(lastNamePartial))
                query = query.Where(r => r.BookerLastname.ToLower().Contains(lastNamePartial.ToLower()));

            if (startDate.HasValue)
                query = query.Where(r => r.ExecutionDate.Date >= startDate.Value.Date);

            if (endDate.HasValue)
                query = query.Where(r => r.ExecutionDate.Date <= endDate.Value.Date);

            if (specificDate.HasValue)
            {
                var targetDate = specificDate.Value.Date;
                query = query.Where(r => r.ExecutionDate.Date == targetDate);
            }

            query = query.OrderBy(r => r.ExecutionDate);

            return await query.ToListAsync();
        }

        public override async Task<Reservation> GetById<IdType>(IdType id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ReservationEquipment)
                    .ThenInclude(re => re.EquipmentType)
                .Include(r => r.Payment)
                .Where(r => r.Id == id.ToString())
                .FirstOrDefaultAsync();

            if (reservation == null)
                throw new NotFoundException("Not found!");

            return reservation;
        }
    }
}