using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Project.Api.Controllers;
using Project.Core.DTO.AccountsDTO;
using Project.Core.DTO.BaseDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IServices;
using Project.Infrastructure.Data;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Core.Interfaces.IServices.IBusinessServices;
using api;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Project.IntegrationTests.Controllers
{
    [TestFixture]
    public class AccountControllerIntegrationTests
    {
        private DataContext _context;
        private AccountController _accountController;
        private Mock<IAccountService> _mockAccountService;
        private UserManager<User> _userManager;

        [SetUp]
        public void Setup()
        {
            // Tworzenie opcji dla in-memory database
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);

            // Tworzenie UserStore i UserManager
            var userStore = new UserStore<User>(_context);
            _userManager = new UserManager<User>(
                userStore,
                null,
                new PasswordHasher<User>(),
                null,
                null,
                null,
                null,
                null,
                null);

            _mockAccountService = new Mock<IAccountService>();
            _accountController = new AccountController(_mockAccountService.Object);
        }

        [Test]
        public async Task CreateAccount_ShouldCreateAccount_WhenValidData()
        {
            var accountDTO = new BaseAccountDTO
            {
                Email = "test@example.com",
                IsAdmin = false
            };

            _mockAccountService.Setup(s => s.CreateAccount(It.IsAny<BaseAccountDTO>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.CreateAccount(accountDTO);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected an OkObjectResult.");

            var okResult = result.Result as OkObjectResult; // Rzutowanie na OkObjectResult
            Assert.That(okResult, Is.Not.Null, "OkObjectResult should not be null.");

            Assert.That(okResult.Value, Is.InstanceOf<SuccessResponseDTO>());
            Assert.That(((SuccessResponseDTO)okResult.Value).Message, Is.EqualTo("Konto założone prawidłowo!"));
        }

        [Test]
        public async Task CreateAccount_ShouldThrow_WhenAccountAlreadyExists()
        {
            // Arrange
            var accountDTO = new BaseAccountDTO
            {
                Email = "test@example.com",
                IsAdmin = false
            };

            _mockAccountService.Setup(s => s.CreateAccount(It.IsAny<BaseAccountDTO>()))
                .Throws(new ApiControlledException("Błąd dodawania konta", 409, "Konto o tym emailu już istnieje"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(async () => await _accountController.CreateAccount(accountDTO));
            Assert.That(ex.Message, Is.EqualTo("Błąd dodawania konta"));
            Assert.That(ex.StatusCode, Is.EqualTo(409));
        }

        [Test]
        public async Task GetAccounts_ShouldReturnOkResult_WithListOfAccounts()
        {
            // Arrange
            var expectedAccounts = new List<GetAccountDTO>
            {
                new GetAccountDTO { Email = "admin@example.com", IsAdmin = true, IsAccountActive = true },
                new GetAccountDTO { Email = "user@example.com", IsAdmin = false, IsAccountActive = false }
            };

            // Ustawienie mocka dla metody GetAccounts
            _mockAccountService.Setup(service => service.GetAccounts())
                .ReturnsAsync(expectedAccounts);

            // Act
            var result = await _accountController.GetAccounts();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>(), "Expected an OkObjectResult.");

            var okResult = result.Result as OkObjectResult; 
            Assert.That(okResult, Is.Not.Null, "OkObjectResult should not be null.");

            var accounts = okResult.Value as List<GetAccountDTO>;
            Assert.That(accounts, Is.Not.Null, "Expected a non-null list of accounts.");
            Assert.That(accounts.Count, Is.EqualTo(expectedAccounts.Count), "Expected account count to match.");
            Assert.That(accounts[0].Email, Is.EqualTo(expectedAccounts[0].Email), "Expected first account email to match.");
            Assert.That(accounts[1].Email, Is.EqualTo(expectedAccounts[1].Email), "Expected second account email to match.");
        }

        [TearDown]
        public void Cleanup()
        {
            // Usuwanie bazy danych po każdym teście
            _context.Database.EnsureDeleted();
            _context.Dispose(); // Zwalnianie kontekstu
            _userManager.Dispose();
        }
    }
}