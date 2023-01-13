using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    
    {
        bool SaveChanges();

        // Colleges
        IEnumerable<College> GetAllColleges();
        void CreateCollege(College college);
        bool PlaformExits(int CollegeId);
        bool ExternalCollegeExists(int externalCollegeId);

        // Commands
        IEnumerable<Command> GetCommandsForCollege(int CollegeId);
        Command GetCommand(int CollegeId, int commandId);
        void CreateCommand(int CollegeId, Command command);
    }
}