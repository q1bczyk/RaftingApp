using Project.Core.DTO.DashboardDTO;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices;

namespace Project.Core.Services.BusinessService
{
    public class DashboardService : IDashboardService
    {
        private readonly IReservationEquipmentRepository _reservationEquipmentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IBaseMapper<EquipmentType, GetEquipmentTypeDTO> _equipmentTypeMapper;
        private readonly IBaseMapper<Reservation, GetReservationDTO> _reservationDTOMapper;

        public DashboardService(IReservationEquipmentRepository reservationEquipmentRepository, IReservationRepository reservationRepository, IBaseMapper<EquipmentType, GetEquipmentTypeDTO> equipmentTypeMapper, IBaseMapper<Reservation, GetReservationDTO> reservationDTOMapper)
        {
            _reservationEquipmentRepository = reservationEquipmentRepository;
            _reservationRepository = reservationRepository;
            _equipmentTypeMapper = equipmentTypeMapper;
            _reservationDTOMapper = reservationDTOMapper;
        }

        public async Task<DashboardDataDTO> GetDashboardData()
        {
            var reservations = await GetReservations();
            var equipment = await GetEquipment();

            DashboardDataDTO dashboardDataDTO = new DashboardDataDTO{
                Reservations = reservations,
                Equipment = equipment,
            };

            return dashboardDataDTO;
        }

        private async Task<List<GetReservationDTO>> GetReservations()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.Date.AddDays(1).AddTicks(-1);

            var reservations = await this._reservationRepository.GetFilteredData(startDate.ToUniversalTime(), endDate.ToUniversalTime());

            return _reservationDTOMapper.MapToList(reservations);
        }

        private async Task<List<GetEquipmentTypeDTO>> GetEquipment()
        {
            var equipment = await this._reservationEquipmentRepository.GetAvailableEquipmentByNow();
            return _equipmentTypeMapper.MapToList(equipment);
        }
    }
}