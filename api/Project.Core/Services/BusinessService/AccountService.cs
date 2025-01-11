using api;
using Microsoft.AspNetCore.Identity;
using Project.Core.DTO.AccountsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        private readonly IMailService _mailService;

        public AccountService(UserManager<User> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task CreateAccount(BaseAccountDTO accountDTO)
        {
            var newUser = new User();
            newUser.UserName = accountDTO.Email.ToLower();
            newUser.Email = accountDTO.Email.ToLower();

            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
                throw new ApiControlledException(string.Join(" ", result.Errors.Select(e => e.Description)), 400);

            string role = accountDTO.IsAdmin ? "admin" : "employee"; 
            var roleResult = await _userManager.AddToRoleAsync(newUser, role);

            if (!roleResult.Succeeded)
                throw new ApiControlledException(string.Join(" ", roleResult.Errors.Select(e => e.Description)), 400);

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            await _mailService.SendConfirmToken(newUser.Email, token, newUser.Id);
        }

        public Task DeleteAccount(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetAccountDTO>> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public Task RoleEdit(string id, bool isAdmin)
        {
            throw new NotImplementedException();
        }
    }
}