using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Project.Api.Controllers;
using Project.Core.DTO.DashboardDTO;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.Interfaces.IServices;

namespace Project.IntegrationTests.Controllers
{
    [TestFixture]
    public class DashboardControllerIntegrationTests
    {
        private Mock<IDashboardService> _mockDashboardService;
        private DashboardController _dashboardController;

        [SetUp]
        public void Setup()
        {
            _mockDashboardService = new Mock<IDashboardService>();
            _dashboardController = new DashboardController(_mockDashboardService.Object);
        }

        [Test]
        public async Task GetDashboardData_ShouldReturnOk_WhenDataIsAvailable()
        {
            // Arrange
            var dashboardData = new DashboardDataDTO
            {
                Reservations = new List<GetReservationDTO>(),
                Equipment = new List<GetEquipmentTypeDTO>()
            };

            _mockDashboardService.Setup(s => s.GetDashboardData()).ReturnsAsync(dashboardData);

            // Act
            var result = await _dashboardController.GetDashboardData();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected an OkObjectResult.");
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf<DashboardDataDTO>(), "Expected DashboardDataDTO.");
            Assert.That(((DashboardDataDTO)okResult.Value).Reservations, Is.EqualTo(dashboardData.Reservations));
            Assert.That(((DashboardDataDTO)okResult.Value).Equipment, Is.EqualTo(dashboardData.Equipment));
        }
    }
}
