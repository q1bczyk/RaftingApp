using Project.Core.DTO.PaymentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.BusinessService
{
    public class ReservationService : BaseCrudService<
    GetReservationDTO,
    AddReservationDTO,
    AddReservationDTO,
    Reservation,
    IReservationRepository
    >, IReservationService
    {
        private readonly IReservationEquipmentService _reservationEquipmentService;
        private readonly IMailService _mailService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBaseMapper<PaymentConfirmationDTO, Payment> _paymentMapper;
        public ReservationService(IReservationRepository repository, IBaseMapper<AddReservationDTO, Reservation> toModelMapper, IBaseMapper<Reservation, GetReservationDTO> toDTOMapper, IReservationEquipmentService reservationEquipmentService, IMailService mailService, IPaymentRepository paymentRepository, IBaseMapper<PaymentConfirmationDTO, Payment> paymentMapper) : base(repository, toModelMapper, toDTOMapper)
        {
            _reservationEquipmentService = reservationEquipmentService;
            _mailService = mailService;
            _paymentRepository = paymentRepository;
            _paymentMapper = paymentMapper;
        }

        public async override Task<GetReservationDTO> Create(AddReservationDTO createDTO)
        {

            var reservationModel = _toModelMapper.MapToModel(createDTO);
            var addedModel = await _repository.Create(reservationModel);
            
            var payment = _paymentMapper.MapToModel(createDTO.Payment);
            payment.ReservationId = addedModel.Id;
            await _paymentRepository.Create(payment);

            await _reservationEquipmentService.AddMany(createDTO.ReservationEquipment, addedModel.Id);

            string formattedDate = createDTO.ExecutionDate.ToString("yyyy-MM-dd");
            await _mailService.SendBookingConfirmation(createDTO.BookerEmail, formattedDate, createDTO.ParticipantNumber, addedModel.Id);

            return _toDTOMapper.MapToModel(reservationModel);
        }
    }
}