using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class Discount : BaseEntity
    {
        [Required]
        public string DiscountName { get; set; }
        [Required]
        public string DiscountDescription { get; set; }
        [Required]
        public string DicountType { get; set; }
        [Required]
        public int DiscountValue { get; set; }
        
        public int? MinCondition { get; set; }
        public int? MaxCondition { get; set; }
    }
}