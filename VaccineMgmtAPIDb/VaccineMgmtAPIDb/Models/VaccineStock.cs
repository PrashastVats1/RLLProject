using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineMgmtAPIDb.Models
{
    public class VaccineStock
    {
        [Key]
        public int Id { get; set; }
        public string? VaccineName { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Stock { get; set; }
    }
}
