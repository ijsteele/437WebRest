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
    public class OrderStatusesController : ControllerBase, iController<OrderStatus>
    {
        private readonly WebRestOracleContext _context;

        public OrderStatusesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> Get()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        // GET: api/Genders/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderStatuses>> Get(string id)
        {
            var order_status = await _context.OrderStatuses.FindAsync(id);

            if (order_status == null)
            {
                return NotFound();
            }

            return order_status;
        }

        // PUT: api/OrderStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderStatus order_status)
        {
            if (id != order_status.OrderStatusId)
            {
                return BadRequest();
            }
            _context.OrderStatuses.Update(order_status);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatus order_status)
        {
            _context.OrderStatuses.Add(order_status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStatus", new { id = order_status.OrderStatusesId }, order_status);
        }

        // DELETE: api/OrderStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order_status = await _context.OrderStatuses.FindAsync(id);
            if (order_status == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(order_status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}
