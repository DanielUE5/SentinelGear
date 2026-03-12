using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentinelGear.Data;
using SentinelGear.Extensions;
using SentinelGear.Models;
using SentinelGear.ViewModels;

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
                .FirstOrDefault(item => item.ProductId == productId);

            if (existingCartItem is not null)
            {
                if (existingCartItem.Quantity < product.StockQuantity)
                {
                    existingCartItem.Quantity++;
                }
            }
            else
            {
                cartItems.Add(new CartItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    StockQuantity = product.StockQuantity
                });
            }

            SaveCartItems(cartItems);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            List<CartItemViewModel> cartItems = GetCartItems();

            CartItemViewModel? cartItemToRemove = cartItems
                .FirstOrDefault(item => item.ProductId == productId);

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
                .FirstOrDefault(item => item.ProductId == productId);

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
            return HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey)
                   ?? new List<CartItemViewModel>();
        }

        private void SaveCartItems(List<CartItemViewModel> cartItems)
        {
            HttpContext.Session.SetObject(CartSessionKey, cartItems);
        }
    }
}