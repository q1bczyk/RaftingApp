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
        private readonly IBaseMapper<RegisterDTO, User> _mapper;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, IBaseMapper<RegisterDTO, User> mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LoggedUserDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null)
                throw new ApiControlledException("Wrong email or password", null, 401);

            var loginSuccess = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(!loginSuccess)
                throw new ApiControlledException("Wrong email or password", null, 401);

            if(!user.EmailConfirmed)
                throw new ApiControlledException("Account is not confirmed", null, 401);

            var token = await _tokenService.CreateToken(user);

            var loggedUser = new LoggedUserDTO{
                Id = user.Id,
                Email = user.Email,
                Token = token
            };

            return loggedUser;
        }

        public Task PasswordReset(BaseAuthDTO passwordResetDTO)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            var newUser = _mapper.MapToModel(registerDTO);
            newUser.UserName = registerDTO.Email.ToLower();

            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if(!result.Succeeded)
                throw new ApiControlledException(string.Join(", ", result.Errors.Select(e => e.Description)), null, 400);
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