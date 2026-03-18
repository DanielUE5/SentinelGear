using Microsoft.EntityFrameworkCore;
using SentinelGear.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static SentinelGear.Common.EntityValidation.Order;

namespace SentinelGear.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(PhoneNumberPattern, ErrorMessage = "Невалиден телефонен номер")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Precision(12, 2)]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        public string? UserId { get; set; }

        // Navigation property for related order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}