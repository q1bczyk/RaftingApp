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

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            await _authService.Register(registerDTO);
            return Ok(new SuccessResponseDTO("Account has been created. Confirmation link has been send on your email"));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoggedUserDTO>> Login(LoginDTO loginDTO)
        {
            var loggedUser = await _authService.Login(loginDTO);
            return Ok(loggedUser);
        }
    }
}