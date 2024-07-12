using Microsoft.AspNetCore.Mvc;
using BW4_progetto.Models;
using BW4_progetto.Services;

namespace BW4_progetto.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            if (cart == null || cart.Items == null)
            {
                cart = new Cart { Items = new List<CartItem>() };
            }
            return View(cart.Items);
        }

        [HttpPost]
        public IActionResult UpdateCartItem(int cartItemId, int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }
            var result = _cartService.UpdateCartItem(cartItemId, quantity);
            return Json(new { success = result });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            _cartService.RemoveFromCart(cartItemId);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(productId, quantity);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = _cartService.GetCart();
            var count = cart?.Items?.Sum(i => i.Quantity) ?? 0;
            return Json(new { count });
        }
    }
}
