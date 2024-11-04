using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase, iController<CustomerAddress>
    {
        private readonly WebRestOracleContext _context;

        public CustomerAddressesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddresses/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerAddress>> Get(string id)
        {
            var customer_address = await _context.CustomerAddresses.FindAsync(id);

            if (customer_address == null)
            {
                return NotFound();
            }

            return customer_address;
        }

        // PUT: api/CustomerAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CustomerAddress customer_address)
        {
            if (id != customer_address.CustomerAddressId)
            {
                return BadRequest();
            }
            _context.CustomerAddresses.Update(customer_address);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> Post(CustomerAddress customer_address)
        {
            _context.CustomerAddresses.Add(customer_address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerAddress", new { id = customer_address.CustomerAddressId }, customer_address);
        }

        // DELETE: api/CustomerAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer_address = await _context.CustomerAddresses.FindAsync(id);
            if (customer_address == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customer_address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}
