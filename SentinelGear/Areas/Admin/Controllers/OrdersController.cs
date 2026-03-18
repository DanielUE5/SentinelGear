using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Models;
using SentinelGear.Models.Enums;
using SentinelGear.ViewModels;

namespace SentinelGear.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly SentinelGearDbContext dbContext;

        public OrdersController(SentinelGearDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .OrderByDescending(o => o.CreatedOn)
                .ThenByDescending(o => o.Id)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Order? order = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.Statuses = GetOrderStatuses(order.Status);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(OrderStatusUpdateViewModel model)
        {
            Order? order = await dbContext.Orders.FindAsync(model.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = model.Status;
            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Статусът на поръчката беше обновен успешно.";
            return RedirectToAction(nameof(Details), new { id = model.OrderId });
        }

        private List<SelectListItem> GetOrderStatuses(OrderStatus selectedStatus)
        {
            return Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = GetStatusDisplayName(status),
                    Selected = status == selectedStatus
                })
                .ToList();
        }

        private string GetStatusDisplayName(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "В процес на изчакване",
                OrderStatus.Processing => "Обработва се",
                OrderStatus.Delivered => "Доставена",
                OrderStatus.Cancelled => "Отказана",
                _ => status.ToString()
            };
        }
    }
}