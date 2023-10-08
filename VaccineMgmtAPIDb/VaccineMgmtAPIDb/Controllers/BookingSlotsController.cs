using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VaccineMgmtAPIDb.Context;
using VaccineMgmtAPIDb.Models;
using Microsoft.EntityFrameworkCore;

namespace VaccineMgmtAPIDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BookingSlotsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var viewSlot = await _appDbContext.BookingSlot.ToListAsync();
            return Ok(viewSlot);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookingSlot([FromBody] BookingSlot addSlot)
        {
            await _appDbContext.BookingSlot.AddAsync(addSlot);
            await _appDbContext.SaveChangesAsync();
            return Ok(addSlot);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookingSlot([FromRoute] int id)
        {
            var slotGet = await _appDbContext.BookingSlot.FirstOrDefaultAsync(x => x.UserId == id);
            if (slotGet== null)
            {
                return NotFound();
            }
            return Ok(slotGet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBookingSlot([FromRoute] int id, BookingSlot bookingSlot)
        {
            var updateSlot = await _appDbContext.BookingSlot.FindAsync(id);
            if (updateSlot == null)
            {
                return NotFound();
            }
            updateSlot.FirstName = bookingSlot.FirstName;
            updateSlot.LastName = bookingSlot.LastName;
            updateSlot.UserName = bookingSlot.UserName;
            updateSlot.Email = bookingSlot.Email;
            updateSlot.Address = bookingSlot.Address;
            updateSlot.Gender = bookingSlot.Gender;
            updateSlot.DateOfBooking = bookingSlot.DateOfBooking;
            updateSlot.City = bookingSlot.City;
            updateSlot.VaccineCentre = bookingSlot.VaccineCentre;
            updateSlot.VaccineName = bookingSlot.VaccineName;

            await _appDbContext.SaveChangesAsync();

            return Ok(updateSlot);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSlot([FromRoute] int id)
        {
            var deleteSlot = await _appDbContext.BookingSlot.FirstOrDefaultAsync(x => x.UserId == id);
            if (deleteSlot == null)
            {
                return NotFound();
            }

            _appDbContext.BookingSlot.Remove(deleteSlot);
            await _appDbContext.SaveChangesAsync();

            return Ok(deleteSlot);
        }
    }
}
