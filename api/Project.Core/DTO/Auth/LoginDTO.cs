using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public class LoginDTO : BaseAuthDTO
    {
        [Required]
        public string Password { get; set; }
    }
}