using System.ComponentModel.DataAnnotations;
using SentinelGear.Models.Enums;

namespace SentinelGear.ViewModels
{
    public class OrderStatusUpdateViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Статус")]
        public OrderStatus Status { get; set; }
    }
}