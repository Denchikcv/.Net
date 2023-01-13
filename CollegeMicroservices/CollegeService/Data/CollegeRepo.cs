using System;
using System.Collections.Generic;
using System.Linq;
using CollegeService.Models;

namespace CollegeService.Data
{
    public class CollegeRepo : ICollegeRepo
    {
        private readonly AppDbContext _context;

        public CollegeRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCollege(College college)
        {
            if(college == null)
            {
                throw new ArgumentNullException(nameof(college));
            }

            _context.Colleges.Add(college);
        }

        public IEnumerable<College> GetAllColleges()
        {
            return _context.Colleges.ToList();
        }

        public College GetCollegeById(int id)
        {
            return _context.Colleges.FirstOrDefault(p => p.Id == id)!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}