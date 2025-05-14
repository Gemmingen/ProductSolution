using Product.Business.Interfaces;
using Product.Business.Models;
using Product.Repository.Entities;
using Product.Repository.Data;

namespace Product.Repository.Repositories;

public class EfProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public EfProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductModel> GetAll() =>
        _context.Products.Select(ToModel).ToList();

    public ProductModel? GetById(int id) =>
        _context.Products.Where(p => p.Id == id).Select(ToModel).FirstOrDefault();

    public ProductModel? GetByName(string name) =>
        _context.Products.Where(p => p.Name == name).Select(ToModel).FirstOrDefault();

    public void Add(ProductModel model)
    {
        var entity = ToEntity(model);
        _context.Products.Add(entity);
        _context.SaveChanges();
        model.Id = entity.Id;
    }

    public void Update(ProductModel model)
    {
        var entity = _context.Products.Find(model.Id);
        if (entity == null) return;

        entity.Name = model.Name;
        entity.Price = model.Price;
        entity.Category = model.Category;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _context.Products.Find(id);
        if (entity == null) return;

        _context.Products.Remove(entity);
        _context.SaveChanges();
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
