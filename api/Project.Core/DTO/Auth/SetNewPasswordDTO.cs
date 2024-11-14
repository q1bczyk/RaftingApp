using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public class SetNewPasswordDTO : ConfirmAccountDTO
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}