using System.ComponentModel.DataAnnotations;
using Project.Core.Validation;

namespace Project.Core.DTO.SettingsDTO
{
    [SeasonActiveValidator]
    [BookingValidator]
    public class BaseSettingsDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Czas wypożycznia musi być większy bądź równy 1")]
        public int HoursRentalTime { get; set; }
        [Required]
        public DateTime SeasonStartDate { get; set; }
        [Required]
        public DateTime SeasonEndDate { get; set; }
        [Required]
        public int DayEarliestBookingTime { get; set; }
        [Required]
        public int DayLatestBookingTime { get; set; }
        [Required, PhoneNumber] 
        public int PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}

