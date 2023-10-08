using System.ComponentModel.DataAnnotations;

namespace VaccineMgmtAPIDb.Models
{
    public class BookingSlot
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBooking { get; set; }
        public string? City { get; set; }
        public string? VaccineCentre { get; set; }
        public string? VaccineName { get; set; }
    }
}
