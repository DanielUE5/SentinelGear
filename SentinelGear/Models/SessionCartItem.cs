using System.ComponentModel.DataAnnotations.Schema;

namespace SentinelGear.Models
{
    [NotMapped]
    public class SessionCartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
