using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineMgmtAPIDb.Context;
using VaccineMgmtAPIDb.Models;

namespace VaccineMgmtAPIDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineStocksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VaccineStocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/VaccineStocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccineStock>>> GetVaccineStock()
        {
          if (_context.VaccineStock == null)
          {
              return NotFound();
          }
            return await _context.VaccineStock.ToListAsync();
        }

        // GET: api/VaccineStocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccineStock>> GetVaccineStock(int id)
        {
          if (_context.VaccineStock == null)
          {
              return NotFound();
          }
            var vaccineStock = await _context.VaccineStock.FindAsync(id);

            if (vaccineStock == null)
            {
                return NotFound();
            }

            return vaccineStock;
        }

        // PUT: api/VaccineStocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccineStock(int id, VaccineStock vaccineStock)
        {
            if (id != vaccineStock.Id)
            {
                return BadRequest();
            }

            _context.Entry(vaccineStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineStockExists(id))
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

        // POST: api/VaccineStocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VaccineStock>> PostVaccineStock(VaccineStock vaccineStock)
        {
          if (_context.VaccineStock == null)
          {
              return Problem("Entity set 'AppDbContext.VaccineStock'  is null.");
          }
            _context.VaccineStock.Add(vaccineStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaccineStock", new { id = vaccineStock.Id }, vaccineStock);
        }

        // DELETE: api/VaccineStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccineStock(int id)
        {
            if (_context.VaccineStock == null)
            {
                return NotFound();
            }
            var vaccineStock = await _context.VaccineStock.FindAsync(id);
            if (vaccineStock == null)
            {
                return NotFound();
            }

            _context.VaccineStock.Remove(vaccineStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VaccineStockExists(int id)
        {
            return (_context.VaccineStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
