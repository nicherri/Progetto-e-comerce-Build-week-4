using Microsoft.AspNetCore.Mvc;
using BW4_progetto.Models;
using BW4_progetto.Services;
using Microsoft.Extensions.Logging;
using System;

namespace BW4_progetto.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ProductService productService, ILogger<AdminController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");
                _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("Model state is not valid.");
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return Json(new { success = true });
        }
    }
}
