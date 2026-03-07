using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.OrderItem;

namespace SentinelGear.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(minQuantity, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Precision(10, 2)]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
