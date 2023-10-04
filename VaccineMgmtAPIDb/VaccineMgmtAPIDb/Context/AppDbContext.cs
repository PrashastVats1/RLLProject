using Microsoft.EntityFrameworkCore;
using VaccineMgmtAPIDb.Models;

namespace VaccineMgmtAPIDb.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
        public DbSet<VaccineMgmtAPIDb.Models.VaccineStock> VaccineStock { get; set; }
    }
}
