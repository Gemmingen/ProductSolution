using Product.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Business.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProductModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task CreateAsync(ProductModel product, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductModel product, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}