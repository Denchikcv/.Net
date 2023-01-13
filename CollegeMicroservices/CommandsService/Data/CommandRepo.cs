using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int collegeId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.CollegeId = collegeId;
            _context.Commands.Add(command);
        }

        public void CreateCollege(College plat)
        {
            if(plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.Colleges.Add(plat);
        }

        public bool ExternalCollegeExists(int externalCollegeId)
        {
            return _context.Colleges.Any(p => p.ExternalID == externalCollegeId);
        }

        public IEnumerable<College> GetAllColleges()
        {
            return _context.Colleges.ToList();
        }

        public Command GetCommand(int collegeId, int commandId)
        {
            return _context.Commands.Where(c => c.CollegeId == collegeId && c.Id == commandId).FirstOrDefault()!;
        }

        public IEnumerable<Command> GetCommandsForCollege(int collegeId)
        {
            return _context.Commands
                .Where(c => c.CollegeId == collegeId)
                .OrderBy( c => c.College.Name);
        }

        public bool PlaformExits(int collegeId)
        {
            return _context.Colleges.Any(p => p.Id == collegeId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}