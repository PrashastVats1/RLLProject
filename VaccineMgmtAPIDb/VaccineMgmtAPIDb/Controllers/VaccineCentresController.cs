using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineMgmtAPIDb.Context;
using VaccineMgmtAPIDb.Models;

namespace VaccineMgmtAPIDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCentresController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public VaccineCentresController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCentres()
        {
            var vaccineCenter = await _appDbContext.VaccineCenter.ToListAsync();
            return Ok(vaccineCenter);
        }

        [HttpPost]
        public async Task<IActionResult> AddCenter([FromBody] VaccineCenter addCenter)
        {
            await _appDbContext.VaccineCenter.AddAsync(addCenter);
            await _appDbContext.SaveChangesAsync();
            return Ok(addCenter);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCenter([FromRoute] int id)
        {
            var center = await _appDbContext.VaccineCenter.FirstOrDefaultAsync(x => x.CenterId == id);
            if (center == null)
            {
                return NotFound();
            }
            return Ok(center);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCenter([FromRoute] int id, VaccineCenter updateCenter)
        {
            var center = await _appDbContext.VaccineCenter.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }
            center.CentreName = updateCenter.CentreName;
            center.City = updateCenter.City;

            await _appDbContext.SaveChangesAsync();

            return Ok(center);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCenter([FromRoute] int id)
        {
            var center = await _appDbContext.VaccineCenter.FirstOrDefaultAsync(x => x.CenterId == id);
            if (center == null)
            {
                return NotFound();
            }

            _appDbContext.VaccineCenter.Remove(center);
            await _appDbContext.SaveChangesAsync();

            return Ok(center);
        }
    }
}
