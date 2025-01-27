using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Project.Core.DTO.AccountsDTO;
using Project.Core.DTO.BaseDTO;
using Project.Core.DTO.EquipmentDTO;
using Xunit;

namespace Project.IntegrationTest.Controllers
{
    public class AccountControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AccountControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateAccount_ReturnsOkResult()
        {
            // Arrange
            var accountDto = new BaseAccountDTO
            {
                Email = "test@example.com",
                IsAdmin = false,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/account", accountDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadFromJsonAsync<SuccessResponseDTO>();
            responseContent.Should().NotBeNull();
            responseContent?.Message.Should().Be("Konto założone prawidłowo!");
        }

        [Fact]
        public async Task GetAccounts_ReturnsOkResultWithAccounts()
        {
            // Act
            var response = await _client.GetAsync("/api/account");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var accounts = await response.Content.ReadFromJsonAsync<List<GetAccountDTO>>();
            accounts.Should().NotBeNull();
            accounts.Should().BeOfType<List<GetAccountDTO>>();
        }

        [Fact]
        public async Task DeleteAccount_ReturnsOkResult()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var response = await _client.DeleteAsync($"/api/account/{email}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadFromJsonAsync<SuccessResponseDTO>();
            responseContent.Should().NotBeNull();
            responseContent?.Message.Should().Be("Konto usunięte prawidłowo");
        }
    }
}