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

        public IActionResult AddProduct(Product product)
        {
            repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
