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

        public override async Task<List<Reservation>> GetAllAsync(){
            var reservations = await _context.Reservations
                .Include(r => r.ReservationEquipment)
                    .ThenInclude(re => re.EquipmentType)
                .ToListAsync();

            return reservations;
        }

        public override async Task<Reservation> GetById<IdType>(IdType id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ReservationEquipment)
                    .ThenInclude(re => re.EquipmentType)
                .Include(r => r.Payment)
                .Where(r => r.Id == id.ToString())
                .FirstOrDefaultAsync();

            if(reservation == null)
                throw new NotFoundException("Not found!");

            return reservation;
        }
    }
}