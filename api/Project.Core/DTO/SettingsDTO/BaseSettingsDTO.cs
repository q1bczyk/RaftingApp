using System.ComponentModel.DataAnnotations;
using Project.Core.Validation;

namespace Project.Core.DTO.SettingsDTO
{
    [OpeningHoursValidator]
    [SeasonActiveValidator]
    [BookingValidator]
    public class BaseSettingsDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "HoursRentalTime must be at least 1.")]
        public int HoursRentalTime { get; set; }
        [Required]
        public DateTime SeasonStartDate { get; set; }
        [Required]
        public DateTime SeasonEndDate { get; set; }
        [Required]
        public int DayEarliestBookingTime { get; set; }
        [Required]
        public int DayLatestBookingTime { get; set; }
        [Required]
        public DateTime OpeningTime { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
        [Required, PhoneNumber] 
        public int PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}