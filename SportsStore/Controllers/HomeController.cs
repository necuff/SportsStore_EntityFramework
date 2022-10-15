using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.Pages;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        private ICategoryRepository catRepository;
        private UptimeService uptimeService;

        public HomeController(IRepository repo, ICategoryRepository catRepo, UptimeService uptimeService)
        {
            repository = repo;
            catRepository = catRepo;
            this.uptimeService = uptimeService;
        }

        public IActionResult Index(QueryOptions options) {       
            ViewBag.UptimeService = uptimeService;
            return View(repository.GetProducts(options));
        }

        public IActionResult UpdateProduct(long key)
        {
            ViewBag.Categories = catRepository.Categories;
            return View(key == 0 ? new Product() : repository.GetProduct(key));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if(product.Id == 0)
            {
                repository.AddProduct(product);
            } else
            {
                repository.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            repository.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;
            return View(nameof(Index), repository.Products);
        }

        [HttpPost]
        public IActionResult UpdateAll(Product[] products)
        {
            repository.UpdateAll(products);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
