using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Route("api/products")]
    public class ProductValuesController : Controller
    {
        private IWebServiceRepository repository;

        public ProductValuesController(IWebServiceRepository repository) => this.repository = repository;

        [HttpGet("{id}")]
        public object GetProduct(long id)
        {
            return repository.GetProduct(id) ?? NotFound();
        }

    }
}
