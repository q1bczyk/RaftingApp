using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class EquipmentType : BaseEntity
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public int MinParticipants { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [Required]
        public int PricePerPerson { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
    }
}