﻿using System.Collections.Generic;
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

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product GetProduct(long key) => context.Products.Find(key);

        public void UpdateAll(Product[] products)
        {
            //Обновляются только изменяемые данные
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = context.Products.Where(p => data.Keys.Contains(p.Id));
            
            foreach (Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            
            //Обновляются все объекты без отслеживания изменений
            //context.Products.UpdateRange(products);
            context.SaveChanges();
        }

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
