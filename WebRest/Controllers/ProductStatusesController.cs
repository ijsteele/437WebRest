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
    public class ProductStatusesController : ControllerBase, iController<ProductStatus>
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatuses/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var product_status = await _context.ProductStatuses.FindAsync(id);

            if (product_status == null)
            {
                return NotFound();
            }

            return product_status;
        }

        // PUT: api/ProductStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus product_status)
        {
            if (id != product_status.ProductStatusId)
            {
                return BadRequest();
            }
            _context.ProductStatuses.Update(product_status);



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

        // POST: api/ProductStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus product_status)
        {
            _context.ProductStatuses.Add(product_status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductStatus", new { id = product_status.ProductStatusId }, product_status);
        }

        // DELETE: api/ProductStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product_status = await _context.ProductStatuses.FindAsync(id);
            if (product_status == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(product_status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
