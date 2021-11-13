using Identity2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity2.Services
{
    public class ProjectService
    {
        private readonly ApplicationContext _context;
        public ProjectService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddDeveloperToProject(int projectId, string developerName)
        {
            var projects = _context.Projects.Where(p => p.Id == projectId).ToList();
            var developer = new Developer() { Name = developerName, Projects = projects };
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Project> GetProjects()
        {
           
            return _context.Projects.ToList();
        }

        public IEnumerable<Project> GetProjectsByDeveloper(string devName)
        {
            return _context.Projects.Where(p => p.Developers.Any(v => v.Name == devName)).Include(x => x.Developers).AsEnumerable();
        }
    }
}
