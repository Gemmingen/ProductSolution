using Product.Business.Models;

namespace Product.Business.Interfaces;

public interface IProductRepository
{
    IEnumerable<ProductModel> GetAll();
    ProductModel? GetById(int id);
    ProductModel? GetByName(string name);
    void Add(ProductModel product);
    void Update(ProductModel product);
    void Delete(int id);
}
