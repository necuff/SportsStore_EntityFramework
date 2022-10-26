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

        [HttpGet]
        public object GetProducts(int skip, int take)
        {
            return repository.GetProducts(skip, take);
        }

        [HttpPost]
        public long StoreProduct([FromBody] Product product)
        {
            return repository.StoreProduct(product);
        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            repository.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            repository.DeleteProduct(id);
        }

    }
}
