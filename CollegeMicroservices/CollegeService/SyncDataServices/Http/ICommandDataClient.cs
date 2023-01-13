using System.Threading.Tasks;
using CollegeService.DTOs;

namespace CollegeService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendCollegeToCommand(CollegeReadDto college); 
    }
}