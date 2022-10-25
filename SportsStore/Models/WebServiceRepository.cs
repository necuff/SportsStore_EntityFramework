using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class WebServiceRepository : IWebServiceRepository
    {
        private DataContext context;

        public WebServiceRepository(DataContext ctx) => this.context = ctx;

        public object GetProduct(long id)
        {
            /*return context.Products.Select(p => new { Id = p.Id, Name = p.Name, 
                Description = p.Description, PurchasePrice = p.PurchasePrice, 
                RetailPrice = p.RetailPrice})
                .FirstOrDefault(p => p.Id == id);
            */

            return context.Products.Include(p => p.Category)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PurchasePrice = p.PurchasePrice,
                    RetailPrice = p.RetailPrice,
                    CategoryId = p.CategoryId,
                    Category = new
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                        Description = p.Category.Description
                    }
                }).FirstOrDefault(p => p.Id == id);
        }
    }
}
