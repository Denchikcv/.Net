using System;
using System.Collections.Generic;
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<ICollegeDataClient>()!;

                var Colleges = grpcClient.ReturnAllColleges();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>()!, Colleges);
            }
        }
        

        private static void SeedData(ICommandRepo repo, IEnumerable<College> College)
        {
            Console.WriteLine("Seeding new collegeforms...");

            foreach (var college in College)
            {
                if(!repo.ExternalCollegeExists(college.ExternalID))
                {
                    repo.CreateCollege(college);
                }
                repo.SaveChanges();
            }
        }
    }
}