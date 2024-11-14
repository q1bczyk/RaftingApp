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
        private readonly IBaseMapper<RegisterDTO, User> _mapper;
        public AuthService(UserManager<User> userManager, IBaseMapper<RegisterDTO, User> mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO)
        {
            throw new NotImplementedException();
        }

        public Task<LoggedUserDTO> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
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
                throw new ApiControlledException(string.Join(", ", result.Errors.Select(e => e.Description)));
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