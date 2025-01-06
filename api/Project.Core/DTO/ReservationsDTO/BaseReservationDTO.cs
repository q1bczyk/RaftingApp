using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.ReservationsDTO.Validator;

namespace Project.Core.DTO.ReservationsDTO
{
    public class BaseReservationDTO
    {
        [Required, DataType(DataType.DateTime)]
        [PastDate]
        public DateTime ExecutionDate { get; set; }
        [Required, MinLength(2)]
        public string BookerName { get; set; }
        [Required, MinLength(2)]
        public string BookerLastname { get; set; }
        [Required]
        public int BookerPhoneNumber { get; set; }
        [Required, EmailAddress]
        public string BookerEmail { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cena nie może być ujemna")]
        public int BookPrice { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Musi być conajmniej jeden uczestnik")]
        public int ParticipantNumber { get; set; }
    }
}