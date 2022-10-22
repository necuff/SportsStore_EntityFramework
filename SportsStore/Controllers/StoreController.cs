using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private IRepository productRepository;
        private ICategoryRepository categoryRepository;

        public StoreController(IRepository repo, ICategoryRepository catRepo)
        {
            productRepository = repo;
            categoryRepository = catRepo;
        }
        
        public IActionResult Index([FromQuery(Name = "options")] 
            QueryOptions productOptions, 
            QueryOptions catOptions, 
            long category)
        {
            ViewBag.Categories = categoryRepository.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;

            return View(productRepository.GetProducts(productOptions, category));
        }
    }
}
