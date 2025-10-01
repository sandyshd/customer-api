using Microsoft.AspNetCore.Mvc;

namespace CustomerAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }

        private static readonly List<Product> Products = Enumerable.Range(1, 20)
            .Select(i => new Product
            {
                Id = i,
                Name = $"Product{i}",
                Description = $"Description for Product{i}",
                Price = 10.0m * i
            })
            .ToList();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(Products);
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Products.Remove(product);
            return Ok("Product deleted successfully");
        }
    }
}
