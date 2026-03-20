using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.Product;

namespace SentinelGear.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [DataType(DataType.Currency)]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        [Range(minStockQuantity, int.MaxValue)]
        public int StockQuantity { get; set; }

        [MaxLength(ManufacturerMaxLength)]
        public string? Manufacturer { get; set; }

        [Url]
        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        // Soft delete property
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
