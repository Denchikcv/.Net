using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Command
    {

        public int Id { get; set; }

        public string? HowTo { get; set; }


        public string? CommandLine { get; set; }

        public int CollegeId { get; set; }

        public College? College {get; set;}
    }
}