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
        private readonly IBaseMapper<RegisterDTO, User> _mapper;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, IBaseMapper<RegisterDTO, User> mapper, IMailService mailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _mailService = mailService;
        }
        public async Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO)
        {
            var user = await _userManager.FindByIdAsync(confirmAccountDTO.UserId);

            if(user == null)
                throw new NotFoundException("User not found");

            if(user.EmailConfirmed)
                throw new ApiControlledException("Account is already confirmed", 400, "Account is already confirmed");

            var result = await _userManager.ConfirmEmailAsync(user, confirmAccountDTO.Token);

            if(!result.Succeeded)
                throw new ApiControlledException("Account activation failed", 400, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<LoggedUserDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null)
                throw new ApiControlledException("Wrong email or password", 401, "Wrong email or password. Enter correct details");

            var loginSuccess = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(!loginSuccess)
                throw new ApiControlledException("Wrong email or password", 401, "Wrong email or password. Enter correct details");

            if(!user.EmailConfirmed)
                throw new ApiControlledException("Account is not confirmed", 401, "Check email and confirm your account");

            var token = await _tokenService.CreateToken(user);

            var loggedUser = new LoggedUserDTO{
                Id = user.Id,
                Email = user.Email,
                Token = token
            };

            return loggedUser;
        }

        public async Task PasswordReset(BaseAuthDTO passwordResetDTO, string requestUrl)
        {
            var user = await _userManager.FindByEmailAsync(passwordResetDTO.Email);

            if(user == null)
                throw new NotFoundException("User not found");

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _mailService.SendPasswordResetToken(user.Email, token, user.Id, requestUrl);
        }

        public async Task Register(RegisterDTO registerDTO, string requestUrl)
        {
            var newUser = _mapper.MapToModel(registerDTO);
            newUser.UserName = registerDTO.Email.ToLower();
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if(!result.Succeeded)
                throw new ApiControlledException(string.Join(" ", result.Errors.Select(e => e.Description)), 400);
            
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            await _mailService.SendConfirmToken(newUser.Email, token, newUser.Id, requestUrl);
        }

        public Task ResendConfirmationToken(BaseAuthDTO confirmationDTO)
        {
            throw new NotImplementedException();
        }

        public Task SetNewPassword(SetNewPasswordDTO setNewPasswordDTO)
        {
            throw new NotImplementedException();
        }

    }
}