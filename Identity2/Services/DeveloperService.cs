using Identity2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity2.Services
{
    public class DeveloperService
    {
        private readonly ApplicationContext _context;
        public DeveloperService(ApplicationContext context)
        {
            _context = context;
        }

        public void AddDeveloper(Developer developer)
        {
            if(!string.IsNullOrEmpty(developer.Name))
                _context.Developers.Add(developer);
        }

        public IQueryable<Developer> GetDevelopersByProject(Project project)
        {
                return _context.Developers.Where(p => p.Projects.Contains(project)).Include(x => x.Projects);
        }

    }
}
