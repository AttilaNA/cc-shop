using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(), SupplierDaoMemory.GetInstance());
        }
        
        public IActionResult Index(string name)
        {
            if (name == null)
            {
                var products = ProductService.GetProductsForCategory(1);
                return View(products.ToList());
            }

            if (ProductService.GetProductCategories().Select(x => x.Name).ToList().Contains(name))
            {
                var productsByCategory = ProductService.GetProductsForCategory(name);
                return View(productsByCategory.ToList());
            }
            var productsBySupplier = ProductService.GetProductsForSupplier(name);
            return View(productsBySupplier.ToList());
        }
        
        public IActionResult Category(string name)
        {
            if (name == null)
            {
                var categories = ProductService.GetProductCategories();
                return PartialView(categories.ToList());
            }
            return Redirect($"Index?name={name}");
        }

        public IActionResult Supplier(string name)
        {
            if (name == null)
            {
                var suppliers = ProductService.GetProductSuppliers();
                return PartialView(suppliers.ToList());
            }

            return Redirect($"Index?name={name}");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult CheckoutForm() => View();

        public IActionResult PaymentForm() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
