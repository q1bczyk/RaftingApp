using api;
using Microsoft.AspNetCore.Identity;
using Project.Core.DTO.Auth;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.OtherServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, IMailService mailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mailService = mailService;
        }
        public async Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO)
        {
            var user = await GetUserById(confirmAccountDTO.UserId);

            if (user.EmailConfirmed)
                throw new ApiControlledException("Account is already confirmed", 400, "Account is already confirmed");

            var result = await _userManager.ConfirmEmailAsync(user, confirmAccountDTO.Token);

            if (!result.Succeeded)
                throw new ApiControlledException("Account activation failed", 400, string.Join(", ", result.Errors.Select(e => e.Description)));

        }

        public async Task<LoggedUserDTO> Login(LoginDTO loginDTO)
        {
            var user = await GetUserByEmail(loginDTO.Email);
            var loginSuccess = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!loginSuccess)
                throw new ApiControlledException("Błędny email lub hasło", 401, "Błędny email lub hasło. Wprowadź poprawne dane");

            if (!user.EmailConfirmed)
                throw new ApiControlledException("Konto nie jest potwierdzone", 401, "Sprawdź maila i potwierdź konto");

            var token = await _tokenService.CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            var loggedUser = new LoggedUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
                Role = roles.FirstOrDefault()
            };

            return loggedUser;
        }

        public async Task PasswordReset(BaseAuthDTO passwordResetDTO)
        {
            var user = await GetUserByEmail(passwordResetDTO.Email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _mailService.SendPasswordResetToken(user.Email, token, user.Id);
        }

        public async Task ResendConfirmationToken(BaseAuthDTO confirmationDTO)
        {
            var user = await GetUserByEmail(confirmationDTO.Email);
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _mailService.SendConfirmToken(user.Email, token, user.Id);
        }

        public async Task SetNewPassword(SetNewPasswordDTO setNewPasswordDTO)
        {
            var user = await GetUserById(setNewPasswordDTO.UserId);
            var result = await _userManager.ResetPasswordAsync(user, setNewPasswordDTO.Token, setNewPasswordDTO.Password);
            if (!result.Succeeded)
                throw new ApiControlledException(string.Join(" ", result.Errors.Select(e => e.Description)), 400);

        }

        public async Task SetPassword(SetPasswordDTO setPasswordDTO)
        {
            var user = await GetUserById(setPasswordDTO.UserId);
            var result = await _userManager.AddPasswordAsync(user, setPasswordDTO.Password);

            if (!result.Succeeded)
                throw new ApiControlledException(string.Join(" ", result.Errors.Select(e => e.Description)), 400);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new ApiControlledException("Użytkownik nie istnieje", 404);

            return user;
        }

        private async Task<User> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ApiControlledException("Użytkownik nie istnieje", 404);

            return user;
        }

    }
}