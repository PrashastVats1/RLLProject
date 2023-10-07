using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineMgmtAPIDb.Context;
using VaccineMgmtAPIDb.Models;

namespace VaccineMgmtAPIDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineStocksController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public VaccineStocksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVaccines()
        {
            var vaccineStock = await _appDbContext.VaccineStock.ToListAsync();
            return Ok(vaccineStock);
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccine([FromBody] VaccineStock addVaccine)
        {
            await _appDbContext.VaccineStock.AddAsync(addVaccine);
            await _appDbContext.SaveChangesAsync();
            return Ok(addVaccine);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetVaccine([FromRoute] int id)
        {
            var vaccine = await _appDbContext.VaccineStock.FirstOrDefaultAsync(x => x.Id == id);
            if(vaccine == null)
            {
                return NotFound();
            }
            return Ok(vaccine);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateVaccine([FromRoute] int id, VaccineStock updateVaccine)
        {
            var vaccine = await _appDbContext.VaccineStock.FindAsync(id);
            if(vaccine == null)
            {
                return NotFound();
            }
            vaccine.VaccineName = updateVaccine.VaccineName;
            vaccine.Manufacturer = updateVaccine.Manufacturer;
            vaccine.ExpiryDate = updateVaccine.ExpiryDate;
            vaccine.Stock = updateVaccine.Stock;

            await _appDbContext.SaveChangesAsync();

            return Ok(vaccine);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVaccine([FromRoute] int id)
        {
            var vaccine = await _appDbContext.VaccineStock.FirstOrDefaultAsync(x => x.Id == id);
            if(vaccine == null)
            {
                return NotFound();
            }

            _appDbContext.VaccineStock.Remove(vaccine);
            await _appDbContext.SaveChangesAsync();

            return Ok(vaccine);
        }
    }
}
