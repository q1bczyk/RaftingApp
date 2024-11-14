using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public abstract class BaseAuthDTO
    {
        [Required]
        public string Email { get; set; }
    }
}