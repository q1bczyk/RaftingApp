using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Project.Core.DTO.Auth;
using Project.Core.Entities;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.OtherServices;

namespace Project.UnitTest.Core.Services.OtherServices
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IMailService> _mailServiceMock;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object,
                                                           null, null, null, null, null, null, null, null);
            _tokenServiceMock = new Mock<ITokenService>();
            _mailServiceMock = new Mock<IMailService>();

            _authService = new AuthService(
                _userManagerMock.Object,
                _tokenServiceMock.Object,
                _mailServiceMock.Object
            );
        }

        [Test]
        public async Task ConfirmAccount_ShouldThrowException_WhenAccountAlreadyConfirmed()
        {
            // Arrange
            var user = new User { EmailConfirmed = true };
            var confirmAccountDTO = new ConfirmAccountDTO { UserId = "userId", Token = "token" };

            _userManagerMock.Setup(um => um.FindByIdAsync(confirmAccountDTO.UserId)).ReturnsAsync(user);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.ConfirmAccount(confirmAccountDTO));
            Assert.That(ex.Message, Is.EqualTo("Account is already confirmed"));
        }

        [Test]
        public async Task ConfirmAccount_ShouldThrowException_WhenConfirmationFails()
        {
            // Arrange
            var user = new User { EmailConfirmed = false };
            var confirmAccountDTO = new ConfirmAccountDTO { UserId = "userId", Token = "token" };
            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error" });

            _userManagerMock.Setup(um => um.FindByIdAsync(confirmAccountDTO.UserId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.ConfirmEmailAsync(user, confirmAccountDTO.Token)).ReturnsAsync(identityResult);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.ConfirmAccount(confirmAccountDTO));
            Assert.That(ex.Message, Is.EqualTo("Account activation failed"));
        }

        [Test]
        public async Task Login_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "notfound@example.com", Password = "password" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDTO.Email)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.Login(loginDTO));
            Assert.That(ex.Message, Is.EqualTo("Błędny email lub hasło"));
        }

        [Test]
        public async Task Login_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User { EmailConfirmed = true };
            var loginDTO = new LoginDTO { Email = "user@example.com", Password = "wrongPassword" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginDTO.Password)).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.Login(loginDTO));
            Assert.That(ex.Message, Is.EqualTo("Błędny email lub hasło"));
        }

        [Test]
        public async Task PasswordReset_ShouldSendResetToken_WhenUserExists()
        {
            // Arrange
            var user = new User { Email = "user@example.com" };
            var passwordResetDTO = new BaseAuthDTO { Email = "user@example.com" };
            var token = "resetToken";

            _userManagerMock.Setup(um => um.FindByEmailAsync(passwordResetDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            _mailServiceMock.Setup(ms => ms.SendPasswordResetToken(user.Email, token, user.Id)).Verifiable();

            // Act
            await _authService.PasswordReset(passwordResetDTO);

            // Assert
            _mailServiceMock.Verify(ms => ms.SendPasswordResetToken(user.Email, token, user.Id), Times.Once);
        }

        [Test]
        public async Task ResendConfirmationToken_ShouldSendConfirmationToken_WhenUserExists()
        {
            // Arrange
            var user = new User { Email = "user@example.com" };
            var confirmationDTO = new BaseAuthDTO { Email = "user@example.com" };
            var token = "confirmationToken";

            _userManagerMock.Setup(um => um.FindByEmailAsync(confirmationDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.GenerateEmailConfirmationTokenAsync(user)).ReturnsAsync(token);
            _mailServiceMock.Setup(ms => ms.SendConfirmToken(user.Email, token, user.Id)).Verifiable();

            // Act
            await _authService.ResendConfirmationToken(confirmationDTO);

            // Assert
            _mailServiceMock.Verify(ms => ms.SendConfirmToken(user.Email, token, user.Id), Times.Once);
        }

        [Test]
        public async Task SetNewPassword_ShouldThrowException_WhenResetFails()
        {
            // Arrange
            var user = new User { Id = "userId" };
            var setNewPasswordDTO = new SetNewPasswordDTO { UserId = "userId", Token = "token", Password = "newPassword" };
            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error" });

            _userManagerMock.Setup(um => um.FindByIdAsync(setNewPasswordDTO.UserId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.ResetPasswordAsync(user, setNewPasswordDTO.Token, setNewPasswordDTO.Password)).ReturnsAsync(identityResult);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.SetNewPassword(setNewPasswordDTO));
            Assert.That(ex.Message, Is.EqualTo("Error"));
        }

        [Test]
        public async Task SetPassword_ShouldThrowException_WhenAddPasswordFails()
        {
            // Arrange
            var user = new User { Id = "userId" };
            var setPasswordDTO = new SetPasswordDTO { UserId = "userId", Password = "newPassword" };
            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error" });

            _userManagerMock.Setup(um => um.FindByIdAsync(setPasswordDTO.UserId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.AddPasswordAsync(user, setPasswordDTO.Password)).ReturnsAsync(identityResult);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ApiControlledException>(() => _authService.SetPassword(setPasswordDTO));
            Assert.That(ex.Message, Is.EqualTo("Error"));
        }
    }
}