using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo) => repository = repo;

        public IActionResult Index() {            
            return View(repository.Products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long key)
        {
            return View(repository.GetProduct(key));
        }
        
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            repository.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
