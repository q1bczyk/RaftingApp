using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.Auth.Validation;

namespace Project.Core.DTO.Auth
{
    public class ConfirmAccountDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "ConfirmPassword must match Password.")]
        public string ConfirmPassword {get; set;}
    }
}