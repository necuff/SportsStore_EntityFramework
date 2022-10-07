using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();
        private DataContext context;
        
        public DataRepository(DataContext context) => this.context = context;


        public IEnumerable<Product> Products => context.Products.ToArray();

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
    }    
}
