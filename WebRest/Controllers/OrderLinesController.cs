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
    public class OrderLinesController : ControllerBase, iController<OrderLine>
    {
        private readonly WebRestOracleContext _context;

        public OrderLinesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrerLines>>> Get()
        {
            return await _context.OrderLines.ToListAsync();
        }

        // GET: api/OrderLines/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderLine>> Get(string id)
        {
            var order_line = await _context.OrderLines.FindAsync(id);

            if (order_line == null)
            {
                return NotFound();
            }

            return order_line;
        }

        // PUT: api/OrderLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderLine order_line)
        {
            if (id != order_line.OrderLineId)
            {
                return BadRequest();
            }
            _context.OrderLines.Update(order_line);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        // POST: api/OrderLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderLine>> Post(OrderLines order_line)
        {
            _context.OrderLines.Add(order_line);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLine", new { id = order_line.OrderLineId }, order_line);
        }

        // DELETE: api/OrderLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order_line = await _context.OrderLines.FindAsync(id);
            if (order_line == null)
            {
                return NotFound();
            }

            _context.OrderLines.Remove(order_line);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrderLines.Any(e => e.OrderLineId == id);
        }
    }
}
