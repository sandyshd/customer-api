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

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>List of customers.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(Customers);
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customer">Customer object to add.</param>
        /// <returns>The created customer.</returns>
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            Customers.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates an existing customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <param name="updatedCustomer">Updated customer object.</param>
        /// <returns>The updated customer.</returns>
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer updatedCustomer)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            return Ok(customer);
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            Customers.Remove(customer);
            return Ok("Customer deleted successfully");
        }
    }
}
