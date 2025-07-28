using Microsoft.AspNetCore.Mvc;
using Product.Business.Models;
using Product.API.Models;
using Product.Business.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        // Abhängigkeit vom Interface statt von der Klasse
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new ProductModel
            {
                Name = request.Name,
                Price = request.Price,
                Category = request.Category
            };

            await _service.CreateAsync(product, cancellationToken);

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _service.GetAllAsync(cancellationToken);
            var response = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
        {
            var product = await _service.GetByIdAsync(id, cancellationToken);
            if (product == null) return NotFound();

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return Ok(response);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetProductByName(string name, CancellationToken cancellationToken)
        {
            var product = await _service.GetByNameAsync(name, cancellationToken);
            if (product == null) return NotFound();

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _service.GetByIdAsync(id, cancellationToken);
            if (product == null) return NotFound();

            product.Price = request.Price;
            // Hinweis: Um mehr als nur den Preis zu aktualisieren, müssten hier mehr Felder zugewiesen werden.
            await _service.UpdateAsync(product, cancellationToken);

            return Ok();
        }
    }
}