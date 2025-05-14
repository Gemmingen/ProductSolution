using Microsoft.EntityFrameworkCore;
using Product.Repository.Entities;

namespace Product.Repository.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
