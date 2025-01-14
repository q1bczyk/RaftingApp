using Moq;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Interfaces.IMapper;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.DTO.PaymentDTO;
using Microsoft.AspNetCore.SignalR;
using Project.Core.SignalR;
using Project.Core.Services.BusinessService;
using NUnit.Framework;

namespace Project.UnitTest.Core
{
    [TestFixture]
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _repositoryMock;
        private Mock<IMailService> _mailServiceMock;
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<IBaseMapper<AddReservationDTO, Reservation>> _toModelMapperMock;
        private Mock<IBaseMapper<Reservation, GetReservationDTO>> _toDTOMapperMock;
        private Mock<IBaseMapper<PaymentConfirmationDTO, Payment>> _paymentMapperMock;
        private Mock<IReservationValidationService> _reservationValidationServiceMock;
        private Mock<IStripeService> _paymentServiceMock;
        private Mock<IHubContext<NotificationHub>> _notificationHubMock;
        private Mock<IClientProxy> _clientProxyMock;

        private ReservationService _service;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IReservationRepository>();
            _mailServiceMock = new Mock<IMailService>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _toModelMapperMock = new Mock<IBaseMapper<AddReservationDTO, Reservation>>();
            _toDTOMapperMock = new Mock<IBaseMapper<Reservation, GetReservationDTO>>();
            _paymentMapperMock = new Mock<IBaseMapper<PaymentConfirmationDTO, Payment>>();
            _reservationValidationServiceMock = new Mock<IReservationValidationService>();
            _paymentServiceMock = new Mock<IStripeService>();
            _notificationHubMock = new Mock<IHubContext<NotificationHub>>();
            _clientProxyMock = new Mock<IClientProxy>();

            _notificationHubMock.Setup(n => n.Clients.Group("admin"))
                .Returns(_clientProxyMock.Object);

            _service = new ReservationService(
                _repositoryMock.Object,
                _toModelMapperMock.Object,
                _toDTOMapperMock.Object,
                _mailServiceMock.Object,
                _paymentRepositoryMock.Object,
                _paymentMapperMock.Object,
                _reservationValidationServiceMock.Object,
                _paymentServiceMock.Object,
                _notificationHubMock.Object
            );
        }

        [Test]
        public async Task Create_ShouldValidateAndSendNotification()
        {
            var createDTO = new AddReservationDTO
            {
                BookerEmail = "test@example.com",
                ExecutionDate = DateTime.UtcNow,
                ParticipantNumber = 5,
                Payment = new PaymentConfirmationDTO { Amount = 100 }
            };

            var reservationModel = new Reservation { Id = "1" };
            var payment = new Payment { ReservationId = "1" };
            var mappedDTO = new GetReservationDTO { Id = "1" };

            _reservationValidationServiceMock.Setup(r => r.ValidReservation(createDTO)).Returns(Task.CompletedTask);
            _toModelMapperMock.Setup(m => m.MapToModel(createDTO)).Returns(reservationModel);
            _repositoryMock.Setup(r => r.Create(reservationModel)).ReturnsAsync(reservationModel);
            _paymentMapperMock.Setup(m => m.MapToModel(createDTO.Payment)).Returns(payment);
            _paymentRepositoryMock.Setup(p => p.Create(payment)).ReturnsAsync(payment);
            _toDTOMapperMock.Setup(m => m.MapToModel(reservationModel)).Returns(mappedDTO);
            _mailServiceMock.Setup(m => m.SendBookingConfirmation(
                createDTO.BookerEmail,
                It.IsAny<string>(),
                createDTO.ParticipantNumber,
                reservationModel.Id)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.Create(createDTO);

            // Assert
            Assert.That(result, Is.EqualTo(mappedDTO));
            _reservationValidationServiceMock.Verify(r => r.ValidReservation(createDTO), Times.Once);
            _mailServiceMock.Verify(m => m.SendBookingConfirmation(
                createDTO.BookerEmail,
                It.IsAny<string>(),
                createDTO.ParticipantNumber,
                reservationModel.Id), Times.Once);
            _clientProxyMock.Verify(c => c.SendCoreAsync("NewNotification", It.IsAny<object[]>(), default), Times.Once);
        }

        [Test]
        public async Task Delete_ShouldRefundPaymentAndDeleteReservation()
        {
            // Arrange
            var reservationId = "1";
            var reservationDetails = new Reservation
            {
                Id = reservationId,
                Payment = new Payment
                {
                    StripeId = "stripe_123",
                    Amount = 100,
                },
                ExecutionDate = DateTime.UtcNow
            };

            _repositoryMock.Setup(r => r.GetById(reservationId))
                .ReturnsAsync(reservationDetails);

            _paymentServiceMock.Setup(p => p.Refund(
                reservationDetails.Payment.StripeId,
                reservationDetails.Payment.Amount,
                reservationDetails.ExecutionDate))
                .Returns(Task.CompletedTask);

            _repositoryMock.Setup(r => r.Delete(reservationDetails))
                .ReturnsAsync(reservationDetails);

            // Act
            await _service.Delete(reservationId);

            // Assert
            _repositoryMock.Verify(r => r.GetById(reservationId), Times.Once);
            _paymentServiceMock.Verify(p => p.Refund(
                reservationDetails.Payment.StripeId,
                reservationDetails.Payment.Amount,
                reservationDetails.ExecutionDate), Times.Once);
            _repositoryMock.Verify(r => r.Delete(reservationDetails), Times.Once);
        }

        [Test]
        public async Task GetFilteredData_ShouldCallRepositoryWithCorrectParameters()
        {
            DateTime? startDate = DateTime.UtcNow.AddDays(-1);
            DateTime? endDate = DateTime.UtcNow;
            string lastNamePartial = "Smith";
            string reservationId = "12345";

            await _service.GetFilteredData(startDate, endDate, null, lastNamePartial, reservationId);

            _repositoryMock.Verify(repo => repo.GetFilteredData(startDate, endDate, null, lastNamePartial, reservationId), Times.Once);
        }
    }
}