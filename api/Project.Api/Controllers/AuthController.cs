using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.Auth;
using Project.Core.DTO.BaseDTO;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoggedUserDTO>> Login(LoginDTO loginDTO)
        {
            var loggedUser = await _authService.Login(loginDTO);
            return Ok(loggedUser);
        }

        [HttpPost("ConfirmAccount")]
        public async Task<ActionResult> ConfirmAccount(ConfirmAccountDTO confirmAccountDTO)
        {
            await _authService.ConfirmAccount(confirmAccountDTO);
            return Ok(new SuccessResponseDTO("Account has been confirmed, now you can sign in"));
        }

        [HttpPost("PasswordReset")]
        public async Task<ActionResult> PasswordReset(BaseAuthDTO passwordResetDTO)
        {
            await _authService.PasswordReset(passwordResetDTO);
            return Ok(new SuccessResponseDTO("Password reset link has been sended. Check your email"));
        }

        [HttpPost("ResendConfirmationToken")]
        public async Task<ActionResult> ResendConfirmationToken(BaseAuthDTO confirmationDTO)
        {
            await _authService.ResendConfirmationToken(confirmationDTO);
            return Ok(new SuccessResponseDTO("Account has been created. Confirmation link has been send on your email"));
        }

        [HttpPost("SetNewPassword")]
        public async Task<ActionResult> SetNewPassword(SetNewPasswordDTO setNewPasswordDTO)
        {
            await _authService.SetNewPassword(setNewPasswordDTO);
            return Ok(new SuccessResponseDTO("Password has been changed"));
        }

        [HttpPost("SetPassword")]
        public async Task<ActionResult> SetPassword(SetPasswordDTO setPasswordDTO)
        {
            await _authService.SetPassword(setPasswordDTO);
            return Ok(new SuccessResponseDTO("Password has been created"));
        }
    }
}