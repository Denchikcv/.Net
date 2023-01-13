using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CollegeService.Models;

namespace CollegeService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            
            if(!context.Colleges.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Colleges.AddRange(
                    new College() {Name="College1", Publisher="Microsoft1", Grade="Free1"},
                    new College() {Name="College2", Publisher="Microsoft2",  Grade="Free2"},
                    new College() {Name="College3", Publisher="Microsoft3",  Grade="Free3"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}