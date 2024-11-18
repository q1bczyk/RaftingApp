using Project.Core.DTO.Auth;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IAuthService
    {
        Task Register(RegisterDTO registerDTO, string requestUrl);
        Task<LoggedUserDTO> Login(LoginDTO loginDTO);
        Task ConfirmAccount(ConfirmAccountDTO confirmAccountDTO);
        Task ResendConfirmationToken(BaseAuthDTO confirmationDTO);
        Task PasswordReset(BaseAuthDTO passwordResetDTO, string requestUrl);
        Task SetNewPassword(SetNewPasswordDTO setNewPasswordDTO);
    }
}