using Product.Business.Interfaces;
using Product.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _repo.GetAllAsync(cancellationToken);

    public async Task<ProductModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _repo.GetByIdAsync(id, cancellationToken);

    public async Task<ProductModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await _repo.GetByNameAsync(name, cancellationToken);

    public async Task CreateAsync(ProductModel product, CancellationToken cancellationToken = default) =>
        await _repo.CreateAsync(product, cancellationToken);

    public async Task UpdateAsync(ProductModel product, CancellationToken cancellationToken = default) =>
        await _repo.UpdateAsync(product, cancellationToken);

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await _repo.DeleteAsync(id, cancellationToken);
}