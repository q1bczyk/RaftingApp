using api;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationEquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Core.Services.BusinessService
{
    public class ReservationEquipmentService : IReservationEquipmentService
    {
        private readonly IReservationEquipmentRepository _repository;
        private readonly IBaseMapper<EquipmentType, GetEquipmentTypeDTO> _equipmentTypeMapper;

        public ReservationEquipmentService(IReservationEquipmentRepository repository, IBaseMapper<EquipmentType, GetEquipmentTypeDTO> equipmentTypeMapper)
        {
            _repository = repository;
            _equipmentTypeMapper = equipmentTypeMapper;
        }

        public async Task AddMany(List<AddReservationEquipmentDTO> addReservationEquipmentDTOs, string reservationId)
        {
            foreach (var reservation in addReservationEquipmentDTOs)
            {
                var dataToAdd = new ReservationEquipment{
                    ReservationId = reservationId,
                    EquipmentTypeId = reservation.EquipmentTypeId,
                    Quantity = reservation.Quantity,
                };

                await _repository.Create(dataToAdd);
            }
        }

        public async Task DeleteAll(string reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSingle(string reservationId, string equipmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetEquipmentTypeDTO>> FetchAvaiableEquipment(ReservationDetailsDTO reservationDetailsDTO)
        {
            var avaiableEquipment = await _repository.GetAvaiableEquipmentAsync(reservationDetailsDTO);
            return _equipmentTypeMapper.MapToList(avaiableEquipment);
        }
    }
}