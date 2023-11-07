using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessing.Product
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetById(int productId);
        
    }
}
