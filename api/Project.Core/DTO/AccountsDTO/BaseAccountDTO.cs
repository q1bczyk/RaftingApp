using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.AccountsDTO
{
    public class BaseAccountDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsAdmin { get; set; }   
    }
}