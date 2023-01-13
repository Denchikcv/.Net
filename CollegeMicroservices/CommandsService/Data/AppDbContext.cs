using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<College> Colleges { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<College>()
                .HasMany(p => p.Commands)
                .WithOne(p=> p.College!)
                .HasForeignKey(p => p.CollegeId);

            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.College)
                .WithMany(p => p.Commands)
                .HasForeignKey(p =>p.CollegeId);
        }
    }
}