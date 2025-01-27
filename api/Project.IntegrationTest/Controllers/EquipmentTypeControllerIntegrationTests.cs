using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Project.Core.DTO.BaseDTO;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Xunit;

namespace Project.IntegrationTest.Controllers
{
    public class EquipmentTypeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EquipmentTypeControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddEquipmentType_ReturnsOkResult()
        {
            // Arrange
            var equipmentType = new AddEquipmentTypeDTO
            {
                TypeName = "Kayak",
                MinParticipants = 2,
                MaxParticipants = 2,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/equipmenttype", equipmentType);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdEquipmentType = await response.Content.ReadFromJsonAsync<GetEquipmentTypeDTO>();
            createdEquipmentType.Should().NotBeNull();
            createdEquipmentType?.TypeName.Should().Be(equipmentType.TypeName);
        }

        [Fact]
        public async Task DeleteEquipmentType_ReturnsOkResult()
        {
            // Arrange
            var id = "test-id";

            // Act
            var response = await _client.DeleteAsync($"/api/equipmenttype/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadFromJsonAsync<SuccessResponseDTO>();
            responseContent.Should().NotBeNull();
            responseContent?.Message.Should().Be("Operacja wykonana prawidłowo");
        }

        [Fact]
        public async Task UpdateEquipmentType_ReturnsOkResult()
        {
            // Arrange
            var id = "test-id";
            var updatedEquipmentType = new AddEquipmentTypeDTO
            {
                TypeName = "Kayak",
                MinParticipants = 2,
                MaxParticipants = 2,
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/equipmenttype/{id}", updatedEquipmentType);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedData = await response.Content.ReadFromJsonAsync<GetEquipmentTypeDTO>();
            updatedData.Should().NotBeNull();
            updatedData?.TypeName.Should().Be(updatedEquipmentType.TypeName);
        }

        [Fact]
        public async Task GetAllEquipment_ReturnsOkResult()
        {
            // Act
            var response = await _client.GetAsync("/api/equipmenttype");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var equipmentList = await response.Content.ReadFromJsonAsync<List<GetEquipmentTypeDTO>>();
            equipmentList.Should().NotBeNull();
            equipmentList.Should().BeOfType<List<GetEquipmentTypeDTO>>();
        }

        [Fact]
        public async Task GetSingleEquipment_ReturnsOkResult()
        {
            // Arrange
            var id = "test-id";

            // Act
            var response = await _client.GetAsync($"/api/equipmenttype/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var equipmentType = await response.Content.ReadFromJsonAsync<GetEquipmentTypeDTO>();
            equipmentType.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAvailableEquipment_ReturnsOkResult()
        {
            // Arrange
            var reservationDetails = new ReservationDetailsDTO
            {
                Date = DateTime.UtcNow.AddDays(2),
                Participants = 4,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/equipmenttype/avaiableEquipment", reservationDetails);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var availableEquipment = await response.Content.ReadFromJsonAsync<List<GetEquipmentTypeDTO>>();
            availableEquipment.Should().NotBeNull();
            availableEquipment.Should().BeOfType<List<GetEquipmentTypeDTO>>();
        }

        [Fact]
        public async Task AddEquipmentType_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var invalidEquipmentType = new AddEquipmentTypeDTO
            {
                TypeName = "",
                MaxParticipants = 0,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/equipmenttype", invalidEquipmentType);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("TypeName");
        }
    }
}