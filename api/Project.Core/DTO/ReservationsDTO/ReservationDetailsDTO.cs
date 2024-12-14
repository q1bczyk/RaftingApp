using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.ReservationsDTO
{
    public class ReservationDetailsDTO
    {
        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Participants must be at least 1.")]
        public int Participants { get; set; }
    }
}