using Microsoft.AspNetCore.Mvc;
using BW4_progetto.Models;
using BW4_progetto.Services;

namespace BW4_progetto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public HomeController(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(); // Ritorna 404 se il prodotto non viene trovato
            }

            return PartialView("~/Views/product/Details.cshtml", product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }

            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                _cartService.AddToCart(productId, quantity);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
