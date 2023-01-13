using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class College
    {
        public int Id { get; set; }
        public int ExternalID { get; set; }
        public string? Name { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
     }
}