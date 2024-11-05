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
    public class AddressTypesController : ControllerBase, iController<AddressType>
    {
        private readonly WebRestOracleContext _context;

        public AddressTypesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var address_type = await _context.AddressTypes.FindAsync(id);

            if (address_type == null)
            {
                return NotFound();
            }

            return address_type;
        }

        // PUT: api/AddressTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressType address_type)
        {
            if (id != address_type.AddressTypeId)
            {
                return BadRequest();
            }
            _context.AddressTypes.Update(address_type);



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

        // POST: api/AddressTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> Post(AddressType address_type)
        {
            _context.AddressTypes.Add(address_type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = address_type.AddressTypeId }, address_type);
        }

        // DELETE: api/AddressType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var address_type = await _context.AddressTypes.FindAsync(id);
            if (address_type == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(address_type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}
