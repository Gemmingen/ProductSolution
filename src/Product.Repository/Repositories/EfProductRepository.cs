using Microsoft.EntityFrameworkCore;
using Product.Business.Interfaces;
using Product.Business.Models;
using Product.Repository.Data;
using Product.Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Repository.Repositories;

public class EfProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public EfProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Products
            .Select(p => ToModel(p))
            .ToListAsync(cancellationToken);

    public async Task<ProductModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.Products
            .Where(p => p.Id == id)
            .Select(p => ToModel(p))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<ProductModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await _context.Products
            .Where(p => p.Name == name)
            .Select(p => ToModel(p))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(ProductModel model, CancellationToken cancellationToken = default)
    {
        var entity = ToEntity(model);
        await _context.Products.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        model.Id = entity.Id;
    }

    public async Task UpdateAsync(ProductModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Products.FindAsync(new object[] { model.Id }, cancellationToken);
        if (entity == null) return;

        entity.Name = model.Name;
        entity.Price = model.Price;
        entity.Category = model.Category;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Products.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return;

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ProductModel ToModel(ProductEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Price = entity.Price,
        Category = entity.Category
    };

    private static ProductEntity ToEntity(ProductModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Price = model.Price,
        Category = model.Category
    };
}