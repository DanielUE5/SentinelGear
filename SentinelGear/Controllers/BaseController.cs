using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SentinelGear.Controllers
{
    public class BaseController : Controller
    {
        public string? GetUserId()
        {
            // This method retrieves the user ID from the claims of the currently authenticated user.
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}