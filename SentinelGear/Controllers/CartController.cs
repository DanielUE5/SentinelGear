using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Extensions;
using SentinelGear.Models;
using SentinelGear.ViewModels;
using SentinelGear.Models.Enums;

namespace SentinelGear.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly SentinelGearDbContext dbContext;

        public CartController(SentinelGearDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId)
        {
            Product? product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == productId && !product.IsDeleted);

            if (product is null)
            {
                return NotFound();
            }

            List<CartItemViewModel> cartItems = GetCartItems();

            CartItemViewModel? existingCartItem = cartItems
                .FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (existingCartItem is not null)
            {
                if (existingCartItem.Quantity < product.StockQuantity)
                {
                    existingCartItem.Quantity++;
                }
            }
            else
            {
                CartItemViewModel newCartItem = new()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    StockQuantity = product.StockQuantity
                };

                cartItems.Add(newCartItem);
            }

            SaveCartItems(cartItems);

            int cartItemsCount = cartItems.Sum(cartItem => cartItem.Quantity);

            return Json(new
            {
                success = true,
                cartCount = cartItemsCount,
                productName = product.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            CartItemViewModel? cartItemToRemove = cartItems
                .FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (cartItemToRemove is not null)
            {
                cartItems.Remove(cartItemToRemove);
                SaveCartItems(cartItems);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            CartItemViewModel? existingCartItem = cartItems
                .FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (existingCartItem is null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (quantity <= 0)
            {
                cartItems.Remove(existingCartItem);
            }
            else
            {
                existingCartItem.Quantity = quantity > existingCartItem.StockQuantity
                    ? existingCartItem.StockQuantity
                    : quantity;
            }

            SaveCartItems(cartItems);

            return RedirectToAction(nameof(Index));
        }

        private List<CartItemViewModel> GetCartItems()
        {
            List<CartItemViewModel>? cartItems = HttpContext.Session
                .GetObject<List<CartItemViewModel>>(CartSessionKey);

            return cartItems ?? new List<CartItemViewModel>();
        }

        private void SaveCartItems(List<CartItemViewModel> cartItems)
        {
            HttpContext.Session.SetObject(CartSessionKey, cartItems);
        }

        public IActionResult Checkout()
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            if (!cartItems.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new CheckoutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            if (!cartItems.Any())
            {
                ModelState.AddModelError(string.Empty, "Количката е празна.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Order order = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                CreatedOn = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = cartItems.Sum(cartItem => cartItem.TotalPrice),
                UserId = User.Identity?.IsAuthenticated == true
                    ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                    : null
            };

            foreach (CartItemViewModel cartItem in cartItems)
            {
                Product? product = await dbContext.Products
                    .FirstOrDefaultAsync(product => product.Id == cartItem.ProductId);

                if (product is null)
                {
                    continue;
                }

                if (product.StockQuantity < cartItem.Quantity)
                {
                    ModelState.AddModelError(string.Empty,
                        $"Недостатъчна наличност за продукт: {product.Name}");
                    return View(model);
                }

                product.StockQuantity -= cartItem.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                });
            }

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction(nameof(Confirmation), new { id = order.Id });
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            Order? order = await dbContext.Orders
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.Product)
            .FirstOrDefaultAsync(order => order.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}