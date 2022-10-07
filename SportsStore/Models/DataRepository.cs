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

        public Product GetProduct(long key) => context.Products.Find(key);

        public void UpdateProduct(Product product)
        {
            //Обновляются только измененные поля
            Product p = GetProduct(product.Id);
            p.Name = product.Name;
            p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            
            //Обновляется весь объект без отслеживания изменений
            //context.Products.Update(product); 
            context.SaveChanges();
        }
    }    
}
