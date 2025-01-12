using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.Auth.Validation;

namespace Project.Core.DTO.Auth
{
    public class SetPasswordDTO
    {
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}