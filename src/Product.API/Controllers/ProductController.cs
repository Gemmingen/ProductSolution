using Microsoft.AspNetCore.Mvc;
using Product.Business.Models;
using Product.Business.Services;
using Product.API.Models;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = new ProductModel
            {
                Name = request.Name,
                Price = request.Price,
                Category = request.Category
            };

            _service.Create(product);

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _service.GetAll().Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        [HttpGet("search/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _service.GetByName(name);
            if (product == null) return NotFound();

            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            product.Price = request.Price;
            _service.Update(product);

            return Ok();
        }
    }
}
