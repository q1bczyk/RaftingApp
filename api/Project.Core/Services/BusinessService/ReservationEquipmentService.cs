using api;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationEquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.BusinessService
{
    public class ReservationEquipmentService : IReservationEquipmentService
    {
        private readonly IReservationEquipmentRepository _repository;
        private readonly IBaseMapper<EquipmentType, GetEquipmentTypeDTO> _equipmentTypeMapper;
        private readonly IFileService _fileService;

        public ReservationEquipmentService(IReservationEquipmentRepository repository, IBaseMapper<EquipmentType, GetEquipmentTypeDTO> equipmentTypeMapper, IFileService fileService)
        {
            _repository = repository;
            _equipmentTypeMapper = equipmentTypeMapper;
            _fileService = fileService;
        }

        public async Task AddMany(List<AddReservationEquipmentDTO> addReservationEquipmentDTOs, string reservationId)
        {
            var newReservations = new List<ReservationEquipment>();

            foreach (var reservation in addReservationEquipmentDTOs)
            {
                Console.WriteLine("RESERVATION: " + reservation.EquipmentTypeId);
                var dataToAdd = new ReservationEquipment
                {
                    ReservationId = reservationId,
                    EquipmentTypeId = reservation.EquipmentTypeId,
                    Quantity = reservation.Quantity,
                    Participants = reservation.Participants,
                };

                newReservations.Add(dataToAdd);
            }

            await _repository.CreateRange(newReservations);
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
            List<GetEquipmentTypeDTO> mappedData = _equipmentTypeMapper.MapToList(avaiableEquipment);
            foreach (var equipmentType in mappedData)
            {
                equipmentType.PhotoUrl = await _fileService.GeneratePublicLink(equipmentType.PhotoUrl);
            }
            return mappedData;
        }
    }
}