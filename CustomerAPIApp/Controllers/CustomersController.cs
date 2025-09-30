using Microsoft.AspNetCore.Mvc;

namespace CustomerAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        public class Customer
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        private static readonly List<Customer> Customers = Enumerable.Range(1, 20)
            .Select(i => new Customer
            {
                Id = i,
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Email = $"customer{i}@example.com"
            })
            .ToList();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(Customers);
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            Customers.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }
    }
}
