using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Project.Core.Entities
{
    public class Settings : BaseEntity
    {
        [Required]
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
        public int PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}