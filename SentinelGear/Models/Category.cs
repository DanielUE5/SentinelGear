using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.Category;

namespace SentinelGear.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        // Navigation property for related products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
