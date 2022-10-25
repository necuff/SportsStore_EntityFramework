using System.Linq;

namespace SportsStore.Models
{
    public class WebServiceRepository : IWebServiceRepository
    {
        private DataContext context;

        public WebServiceRepository(DataContext ctx) => this.context = ctx;

        public object GetProduct(long id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
