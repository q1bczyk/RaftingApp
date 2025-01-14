using api;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Project.Core.DTO.AccountsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.BusinessService;

namespace Project.UnitTest.Core
{
    public class AccountServiceTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<IMailService> _mailServiceMock;
        private AccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object,
                                                           null, null, null, null, null, null, null, null);
            _mailServiceMock = new Mock<IMailService>();
            _accountService = new AccountService(_userManagerMock.Object, _mailServiceMock.Object);
        }

        [Test]
        public async Task CreateAccount_ShouldCreateUser_WhenEmailDoesNotExist()
        {
            // Arrange
            var accountDTO = new BaseAccountDTO { Email = "test@example.com", IsAdmin = false };
            var user = new User { Id = "1", UserName = accountDTO.Email, Email = accountDTO.Email };

            _userManagerMock.Setup(um => um.FindByEmailAsync(accountDTO.Email)).ReturnsAsync((User)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");

            // Act
            await _accountService.CreateAccount(accountDTO);

            // Assert
            _userManagerMock.Verify(um => um.FindByEmailAsync(accountDTO.Email), Times.Once);
            _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>()), Times.Once);
            _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), "employee"), Times.Once);
            _mailServiceMock.Verify(ms => ms.SendConfirmToken(accountDTO.Email, "token", It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task CreateAccount_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var accountDTO = new BaseAccountDTO { Email = "test@example.com", IsAdmin = false };
            var existingUser = new User { Id = "1", UserName = accountDTO.Email, Email = accountDTO.Email };

            _userManagerMock.Setup(um => um.FindByEmailAsync(accountDTO.Email)).ReturnsAsync(existingUser);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _accountService.CreateAccount(accountDTO));
            Assert.That(ex.Details, Is.EqualTo("Konto o tym emailu już istnieje"));
        }

        [Test]
        public async Task DeleteAccount_ShouldDeleteUser_WhenUserExistsAndIsNotLastAdmin()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Id = "1", UserName = email, Email = email };
            var roles = new List<string> { "employee" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            _userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            await _accountService.DeleteAccount(email);

            // Assert
            _userManagerMock.Verify(um => um.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteAccount_ShouldThrowException_WhenDeletingLastAdmin()
        {
            var email = "admin@example.com";
            var user = new User { Id = "1", UserName = email, Email = email };

            var roles = new List<string> { "admin" };
            _userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
        
            var adminUsers = new List<User> { user };
            _userManagerMock.Setup(um => um.Users).Returns(adminUsers.AsQueryable());

            var ex =  Assert.ThrowsAsync<ApiControlledException>(() => _accountService.DeleteAccount(email));
            Assert.That(ex.Details, Is.EqualTo("Nie można usunąć ostatniego administratora!"));
        }

        [Test]
        public async Task GetAccounts_ShouldReturnListOfAccounts()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Email = "user1@example.com", EmailConfirmed = true },
                new User { Id = "2", Email = "admin@example.com", EmailConfirmed = false },
            };

            _userManagerMock.Setup(um => um.Users).Returns(users.AsQueryable());

            _userManagerMock.Setup(um => um.GetRolesAsync(users[0])).ReturnsAsync(new List<string> { "employee" });
            _userManagerMock.Setup(um => um.GetRolesAsync(users[1])).ReturnsAsync(new List<string> { "admin" });

            // Act
            var result = await _accountService.GetAccounts();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Has.Some.Matches<GetAccountDTO>(a => a.Email == "user1@example.com" && a.IsAdmin == false)); // Zamiast Assert.IsTrue
            Assert.That(result, Has.Some.Matches<GetAccountDTO>(a => a.Email == "admin@example.com" && a.IsAdmin == true)); // Zamiast Assert.IsTrue
        }
    }
}