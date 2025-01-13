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
            var user = await _userManager.FindByEmailAsync(accountDTO.Email);
            if (user != null)
                throw new ApiControlledException("Błąd dodawania konta", 409, "Konto o tym emailu już istnieje");
                
            var newUser = new User();
            newUser.UserName = accountDTO.Email;
            newUser.Email = accountDTO.Email;

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

        public async Task DeleteAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.FirstOrDefault() == "admin")
            {
                int adminCount = 0; 
                var users = _userManager.Users.ToList();
                foreach (var item in users)
                {
                    var roles = await _userManager.GetRolesAsync(item);
                    if(roles.FirstOrDefault() == "admin") adminCount++;
                }
                if(adminCount <= 1)
                    throw new ApiControlledException("Błąd usuwania konta", 409, "Nie można usunąc ostatniego administratora!");
            }

            await _userManager.DeleteAsync(user);
        }

        public async Task<List<GetAccountDTO>> GetAccounts()
        {
            var users = _userManager.Users.ToList();
            var usersWithRoles = new List<GetAccountDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new GetAccountDTO
                {
                    Email = user.Email,
                    IsAdmin = roles.FirstOrDefault() == "admin",
                    IsAccountActive = user.EmailConfirmed,
                });
            }

            return usersWithRoles;
        }

        public Task RoleEdit(string id, bool isAdmin)
        {
            throw new NotImplementedException();
        }
    }
}