using Microsoft.AspNetCore.Mvc;
using Source.Models;

namespace Source.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1,Name="Iphone 14",Price=3300},
            new Product { Id = 2,Name="Macbook M1 Pro",Price=4200},
            new Product { Id = 3,Name="Tesla Model 3",Price=65000}
        };
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(_products);
        }

        [HttpPost]
        public IActionResult Add(Product addProduct)
        {
            var product = new Product()
            {
                Id = addProduct.Id,
                Name = addProduct.Name,
                Price = addProduct.Price
            };
            _products.Add(product);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product is not null)
            {
                var myProduct = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
                return await Task.Run(() => View("View", myProduct));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult View(Product updatedProduct)
        {
            var product = _products.Find(id => updatedProduct.Id == _products.IndexOf(id));
            if (product is not null)
            {
                var newProduct = new Product()
                {
                    Id = updatedProduct.Id,
                    Name = updatedProduct.Name,
                    Price = updatedProduct.Price
                };
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string searchInput)
        {
            var filteredProducts = _products.Where(p => p.Name.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            return View(filteredProducts);
        }

        [HttpPost]
        public IActionResult Delete(Product findProduct)
        {
            var product = _products.Find(id=>findProduct.Id==_products.IndexOf(id));
            if (product is not null)
                _products.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
