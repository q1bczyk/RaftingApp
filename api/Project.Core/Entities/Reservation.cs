using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class Reservation : BaseEntity
    {
        [Required]
        public DateTime ExecutionDate { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string BookerName { get; set; }
        [Required]
        public string BookerLastname { get; set; }
        [Required]
        public int BookerPhoneNumber { get; set; }
        [Required]
        public int BookPrice { get; set; }
        [Required]
        public int ParticipantNumber { get; set; }
        [Required]
        public Payment Payment { get; set; }
        public IEnumerable<EquipmentType> EquipmentType { get; set; } = [];
        public IEnumerable<ReservationEquipment> ReservationEquipment { get; } = [];

    }
}