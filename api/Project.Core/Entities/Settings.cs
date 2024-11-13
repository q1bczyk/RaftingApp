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
        public DateTime OpeningTime { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
    }
}