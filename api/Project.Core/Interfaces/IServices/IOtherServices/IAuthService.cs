using Project.Core.DTO.Auth;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IAuthService
    {
        Task Register(RegisterDTO registerDTO);
        Task<LoggedUserDTO> Login(LoginDTO loginDTO);
        Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO);
        Task ResendConfirmationToken(BaseAuthDTO confirmationDTO);
        Task PasswordReset(BaseAuthDTO passwordResetDTO);
        Task SetNewPassword(SetNewPasswordDTO setNewPasswordDTO);
    }
}