using Project.Core.DTO.AccountsDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IAccountService
    {
        Task<List<GetAccountDTO>> GetAccounts();
        Task CreateAccount(BaseAccountDTO accountDTO);
        Task DeleteAccount(string id);
        Task RoleEdit(string id, bool isAdmin);
    }
}