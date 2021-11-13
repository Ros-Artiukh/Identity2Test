using Identity2.Models;
using Identity2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Identity2.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, ProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            var projects = _projectService.GetProjects();
            return View(projects);
        }

        [HttpPost]
        public async Task<string> AddDeveloperToProject(int projectId, string developerName)
        {
            await _projectService.AddDeveloperToProject(projectId, developerName);
            return "succesfully added";
        }


        [HttpPost]
        public IActionResult GetProjectsByDeveloper(string developer)
        {
            var result = _projectService.GetProjectsByDeveloper(developer);
            return View("Projects", result);
        }

    }
}
