using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.AccountsDTO;
using Project.Core.DTO.BaseDTO;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Api.Controllers
{
    [Authorize(Policy = "AdminOnly")] 
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<SuccessResponseDTO>> CreateAccount(BaseAccountDTO accountDTO)
        {
            await _accountService.CreateAccount(accountDTO);
            return Ok(new SuccessResponseDTO("Konto założone prawidłowo!"));
        }
    }
}