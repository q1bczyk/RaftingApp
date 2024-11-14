using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string RepetedPassword { get; set; }
    }
}