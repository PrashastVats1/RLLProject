using System.ComponentModel.DataAnnotations;

namespace VaccineMgmtAPIDb.Models
{
    public class VaccineCenter
    {
        [Key]
        public int CenterId { get; set; }
        public string? CentreName { get; set; }
        public string? City { get; set; }
    }
}
