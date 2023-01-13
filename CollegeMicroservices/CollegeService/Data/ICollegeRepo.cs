using System.Collections.Generic;
using CollegeService.Models;

namespace CollegeService.Data
{
    public interface ICollegeRepo
    {
        bool SaveChanges();

        IEnumerable<College> GetAllColleges();
        College GetCollegeById(int id);
        void CreateCollege(College college);
    }
}