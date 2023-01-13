using Microsoft.EntityFrameworkCore;
using CollegeService.Models;

namespace CollegeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<College> Colleges { get; set; }
    }
}