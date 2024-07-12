using Microsoft.AspNetCore.Mvc;
using BW4_progetto.Models;
using BW4_progetto.Services;
using Microsoft.Extensions.Logging;

namespace BW4_progetto.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            _logger.LogInformation($"Details action called with id: {id}");

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id: {id} not found.");
                return NotFound(); // Aggiungi questo controllo per evitare errori nulli
            }

            _logger.LogInformation($"Product found: {product.Name}");
            return PartialView("~/Views/Product/Details.cshtml", product);
        }
    }
}
