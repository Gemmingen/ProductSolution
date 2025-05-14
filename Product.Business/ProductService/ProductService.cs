using Product.Business.Interfaces;
using Product.Business.Models;

namespace Product.Business.Services;

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<ProductModel> GetAll() => _repo.GetAll();
    public ProductModel? GetById(int id) => _repo.GetById(id);
    public ProductModel? GetByName(string name) => _repo.GetByName(name);
    public void Create(ProductModel product) => _repo.Add(product);
    public void Update(ProductModel product) => _repo.Update(product);
    public void Delete(int id) => _repo.Delete(id);
}
