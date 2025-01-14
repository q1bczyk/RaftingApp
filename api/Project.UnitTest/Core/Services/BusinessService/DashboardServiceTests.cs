using Moq;
using NUnit.Framework;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Services.BusinessService;

namespace Project.UnitTest.Core
{
    public class DashboardServiceTests
    {
        private Mock<IReservationEquipmentRepository> _reservationEquipmentRepositoryMock;
        private Mock<IReservationRepository> _reservationRepositoryMock;
        private Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>> _equipmentTypeMapperMock;
        private Mock<IBaseMapper<Reservation, GetReservationDTO>> _reservationDTOMapperMock;
        private DashboardService _dashboardService;

        [SetUp]
        public void Setup()
        {
            _reservationEquipmentRepositoryMock = new Mock<IReservationEquipmentRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _equipmentTypeMapperMock = new Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>>();
            _reservationDTOMapperMock = new Mock<IBaseMapper<Reservation, GetReservationDTO>>();

            _dashboardService = new DashboardService(
                _reservationEquipmentRepositoryMock.Object,
                _reservationRepositoryMock.Object,
                _equipmentTypeMapperMock.Object,
                _reservationDTOMapperMock.Object);
        }

        [Test]
        public async Task GetDashboardData_ShouldReturnDashboardDataDTO_WithReservationsAndEquipment()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new Reservation { /* initialize properties */ },
                new Reservation { /* initialize properties */ }
            };
            var equipment = new List<EquipmentType>
            {
                new EquipmentType { /* initialize properties */ },
                new EquipmentType { /* initialize properties */ }
            };

            var expectedReservationsDTO = new List<GetReservationDTO>
            {
                new GetReservationDTO { /* initialize properties */ },
                new GetReservationDTO { /* initialize properties */ }
            };

            var expectedEquipmentDTO = new List<GetEquipmentTypeDTO>
            {
                new GetEquipmentTypeDTO { /* initialize properties */ },
                new GetEquipmentTypeDTO { /* initialize properties */ }
            };

            _reservationRepositoryMock.Setup(repo => repo.GetTodayReservations()).ReturnsAsync(reservations);
            _reservationDTOMapperMock.Setup(mapper => mapper.MapToList(reservations)).Returns(expectedReservationsDTO);
            _reservationEquipmentRepositoryMock.Setup(repo => repo.GetAvailableEquipmentByNow()).ReturnsAsync(equipment);
            _equipmentTypeMapperMock.Setup(mapper => mapper.MapToList(equipment)).Returns(expectedEquipmentDTO);

            // Act
            var result = await _dashboardService.GetDashboardData();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Reservations, Is.EqualTo(expectedReservationsDTO));
            Assert.That(result.Equipment, Is.EqualTo(expectedEquipmentDTO));
        }
    }
}