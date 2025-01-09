using Microsoft.AspNetCore.SignalR;
using Project.Core.DTO.PaymentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.SignalR;

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
        private readonly IReservationValidationService _reservationValidationService;
        private readonly IStripeService _paymentService;
        IHubContext<NotificationHub> _notificationHub;
        
        public ReservationService(IReservationRepository repository, IBaseMapper<AddReservationDTO, Reservation> toModelMapper, IBaseMapper<Reservation, GetReservationDTO> toDTOMapper, IReservationEquipmentService reservationEquipmentService, IMailService mailService, IPaymentRepository paymentRepository, IBaseMapper<PaymentConfirmationDTO, Payment> paymentMapper, IReservationValidationService reservationValidationService, IStripeService paymentService, IHubContext<NotificationHub> notificationHub) : base(repository, toModelMapper, toDTOMapper)
        {
            _reservationEquipmentService = reservationEquipmentService;
            _mailService = mailService;
            _paymentRepository = paymentRepository;
            _paymentMapper = paymentMapper;
            _reservationValidationService = reservationValidationService;
            _paymentService = paymentService;
            _notificationHub = notificationHub;
        }

        public async override Task<GetReservationDTO> Create(AddReservationDTO createDTO)
        {
            await _reservationValidationService.ValidReservation(createDTO);
            var reservationModel = _toModelMapper.MapToModel(createDTO);
            var addedModel = await _repository.Create(reservationModel);
            
            var payment = _paymentMapper.MapToModel(createDTO.Payment);
            payment.ReservationId = addedModel.Id;
            await _paymentRepository.Create(payment);

            await _reservationEquipmentService.AddMany(createDTO.ReservationEquipment, addedModel.Id);

            string formattedDate = createDTO.ExecutionDate.ToString("yyyy-MM-dd");
            await _mailService.SendBookingConfirmation(createDTO.BookerEmail, formattedDate, createDTO.ParticipantNumber, addedModel.Id);

            var mappedReservation = _toDTOMapper.MapToModel(reservationModel);

            await _notificationHub.Clients
                    .Group("admin")
                    .SendAsync("NewNotification", mappedReservation);

            return mappedReservation;
        }

        public async override Task Delete(string id){
            var reservationDetails = await _repository.GetById(id);
            await _paymentService.Refund(reservationDetails.Payment.StripeId, reservationDetails.Payment.Amount, reservationDetails.ExecutionDate);
            await _repository.Delete(reservationDetails);
        }

        public async Task<List<GetReservationDTO>> GetFilteredData(DateTime? startDate = null, DateTime? endDate = null, DateTime? specificDate = null, string lastNamePartial = null, string reservationId = null)
        {
            var filteredReservations = await _repository.GetFilteredData(startDate, endDate, specificDate, lastNamePartial, reservationId);
            return _toDTOMapper.MapToList(filteredReservations);
        }
    }
}