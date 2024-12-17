using api;
using Microsoft.EntityFrameworkCore;
using Project.Core.DTO.ReservationsDTO;
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

        public async Task<List<EquipmentType>> GetAvaiableEquipmentAsync(ReservationDetailsDTO reservationDetailsDTO)
        {
            var reservationDateUtc = reservationDetailsDTO.Date.ToUniversalTime();

            var allEquipment = await _context.EquipmentTypes
                .OrderBy(eq => eq.MinParticipants)
                .ToListAsync();

            var reservations = await _context.ReservationsEquipment
                .Include(re => re.Reservation)
                .Where(re => re.Reservation.ExecutionDate > reservationDateUtc.AddHours(-4) && re.Reservation.ExecutionDate < reservationDateUtc.AddHours(4))
                .ToListAsync();

            var availableQuantities = allEquipment.ToDictionary(e => e.Id, e => e.Quantity);

            foreach (var reservation in reservations)
                if (availableQuantities.ContainsKey(reservation.EquipmentTypeId))
                    availableQuantities[reservation.EquipmentTypeId] -= reservation.Quantity;

            int maxParticipants = 0;
            int minParticipants = 1;

            foreach (var equipmentType in allEquipment)
            {
                equipmentType.Quantity = availableQuantities[equipmentType.Id];
                maxParticipants += equipmentType.Quantity * equipmentType.MaxParticipants;
                if(equipmentType.Quantity > 0 && minParticipants > equipmentType.MinParticipants)
                    minParticipants = equipmentType.MinParticipants;
            }

            if(reservationDetailsDTO.Participants > maxParticipants) 
                throw new ApiControlledException("Brak dostępnego sprzętu we wskazanym terminie.", 409, "Spróbuj w innym terminie");

            if(reservationDetailsDTO.Participants < minParticipants) 
                throw new ApiControlledException("Brak dostępnego sprzętu we wskazanym terminie.", 409, "Spróbuj w innym terminie");


            return allEquipment;
        }
    }
}