namespace Product.API.Models;

public class CreateProductRequest
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string Category { get; set; } = default!;
}
