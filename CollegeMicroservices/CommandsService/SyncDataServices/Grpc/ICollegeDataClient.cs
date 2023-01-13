using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc
{
    public interface ICollegeDataClient
    {
        IEnumerable<College> ReturnAllColleges();
    }
}