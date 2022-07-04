using Catalog.Api.Entities;

namespace Catalog.Api.Repositories {
    public interface IProductRepository {

        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task CreateProduct(Product command);
        Task<bool> UpdateProduct(Product command);
        Task<bool> DeleteProduct(string id);


    }
}
