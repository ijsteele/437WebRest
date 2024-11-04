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
    public class OrderStatesController : ControllerBase, iController<OrderState>
    {
        private readonly WebRestOracleContext _context;

        public OrderStatesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> Get()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderState>> Get(string id)
        {
            var order_state = await _context.OrderStates.FindAsync(id);

            if (order_state == null)
            {
                return NotFound();
            }

            return order_state;
        }

        // PUT: api/OrderState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderState order_state)
        {
            if (id != order_state.OrderStateId)
            {
                return BadRequest();
            }
            _context.OrderStates.Update(order_state);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
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

        // POST: api/OrderState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> Post(OrderState order_state)
        {
            _context.OrderStates.Add(order_state);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderState", new { id = order_state.OrderStateId }, order_state);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order_state = await _context.OrderStates.FindAsync(id);
            if (order_state == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(order_state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}
