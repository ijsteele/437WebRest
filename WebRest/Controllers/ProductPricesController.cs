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
    public class ProductPricesController : ControllerBase, iController<ProductPrice>
    {
        private readonly WebRestOracleContext _context;

        public ProductPricesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> Get()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrices/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductPrice>> Get(string id)
        {
            var price = await _context.ProductPrices.FindAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return price;
        }

        // PUT: api/ProductPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductPrice price)
        {
            if (id != price.ProductPriceId)
            {
                return BadRequest();
            }
            _context.ProductPrices.Update(price);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
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

        // POST: api/ProductPrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> Post(ProductPrice price)
        {
            _context.ProductPrices.Add(price);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPrice", new { id = price.ProductPriceId }, price);
        }

        // DELETE: api/ProductPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var price = await _context.ProductPrices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(price);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }
    }
}
