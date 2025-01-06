using api;
using Project.Core.DTO.ReservationEquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.OtherServices
{
    public class ReservationValidationService : IReservationValidationService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;
        public ReservationValidationService(ISettingsRepository settingsRepository, IEquipmentTypeRepository equipmentTypeRepository)
        {
            _settingsRepository = settingsRepository;
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public async Task ValidReservation(AddReservationDTO reservationDetails)
        {
            var systemSettings = await _settingsRepository.GetSettingsAsync();
            IsDataCorrect(reservationDetails.ExecutionDate, systemSettings.SeasonStartDate, systemSettings.SeasonEndDate);
            IsOpeningHoursCorrect(reservationDetails.ExecutionDate, systemSettings.SeasonStartDate, systemSettings.SeasonEndDate);
            IsReservationInCorrectAdvance(systemSettings.DayEarliestBookingTime, systemSettings.DayLatestBookingTime, reservationDetails.ExecutionDate);
            await IsPersonLimitMet(reservationDetails.ReservationEquipment);
        }
        private void IsDataCorrect(DateTime executionDate, DateTime openingDate, DateTime closingDate)
        {
            DateTime seasonStart = new DateTime(1, openingDate.Month, openingDate.Day);
            DateTime seasonEnd = new DateTime(1, closingDate.Month, closingDate.Day);
            DateTime execution = new DateTime(1, executionDate.Month, executionDate.Day);

            if (execution < seasonStart || execution > seasonEnd)
                throw new ApiControlledException("Niepoprawna data spływu", 409, "W podanym terminie spływ jest niedostępny.");

        }

        private void IsOpeningHoursCorrect(DateTime executionDate, DateTime openingDate, DateTime closeingDate)
        {
            TimeSpan executionTime = executionDate.TimeOfDay;
            TimeSpan openingTime = openingDate.TimeOfDay;
            TimeSpan closeTime = closeingDate.TimeOfDay;

            if (executionTime < openingTime || executionTime > closeTime)
                throw new ApiControlledException("Niepoprawna godzina spływu", 409, "O podanej godzinie spływ jest niedostępny");
        }

        private async Task IsPersonLimitMet(List<AddReservationEquipmentDTO> equipment)
        {
            foreach (var equipmentItem in equipment)
            {
                var equipmentDetails = await _equipmentTypeRepository.GetById(equipmentItem.EquipmentTypeId);

                if (equipmentItem.Participants > equipmentDetails.MaxParticipants * equipmentItem.Quantity)
                    throw new ApiControlledException("Niepoprawna dobranie sprzętu", 409, $"Liczba uczestników przekracza maksymalna liczbę uczestników dla sprzętu: {equipmentDetails.TypeName}");

                if (equipmentItem.Participants < equipmentDetails.MinParticipants * equipmentItem.Quantity)
                    throw new ApiControlledException("Niepoprawna dobranie sprzętu", 409, $"Liczba uczestników nie spełnia minimalnej liczby uczestników dla sprzętu: {equipmentDetails.TypeName}");
            }
        }

        private void IsReservationInCorrectAdvance(int dayEarliest, int dayLatest, DateTime executionDate)
        {
            DateTime today = DateTime.Now;

            if (executionDate < today.AddHours(dayLatest * 24))
                throw new ApiControlledException("Za późno na dokonanie rezerwacji", 409, $"Maksymalne wyprzedzenie z jaką można dokonać rezerwacji to: {dayLatest} doby");

            if (executionDate > today.AddDays(dayEarliest))
                throw new ApiControlledException("Za wcześnie na dokonanie rezerwacji", 409, $"Minimalne wyprzedzenie z jaką można dokonać rezerwacji to: {dayEarliest} doby");
        }

    }
}