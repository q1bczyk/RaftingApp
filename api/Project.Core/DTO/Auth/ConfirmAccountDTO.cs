using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.Auth
{
    public class ConfirmAccountDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
    }
}