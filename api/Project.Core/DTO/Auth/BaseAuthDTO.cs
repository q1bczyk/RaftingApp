using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public class BaseAuthDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}