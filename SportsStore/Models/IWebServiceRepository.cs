namespace SportsStore.Models
{
    public interface IWebServiceRepository
    {
        object GetProduct(long id);

        object GetProducts(int skip, int take);
    }
}
