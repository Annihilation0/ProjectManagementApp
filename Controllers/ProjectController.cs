using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models.ViewModels;
using ProjectManagementApp.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DBContext context;
        public ProjectController(DBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {

            var projects = context.Projects
            .Include(p => p.ClientCompany)
            .Include(p => p.ExecutionCompany)
            .Include(p => p.ProjectManager)
            .Select(p => new ProjectViewModel
            {
                ProjectID = p.ProjectID,
                ProjectName = p.ProjectName,
                ClientCompanyName = p.ClientCompany.CompanyName,
                ExecutionCompanyName = p.ExecutionCompany.CompanyName,
                ProjectManagerFullName = $"{p.ProjectManager.FirstName} {p.ProjectManager.MiddleName} {p.ProjectManager.LastName}",
                StartDate = p.StartDate.ToShortDateString(),
                EndDate = p.EndDate.ToShortDateString(),
                Priority = p.Priority
            });

            return View("Projects", projects);

        }
        public IActionResult Details(int projectID)
        {
            var project = context.Projects
        .Include(p => p.ClientCompany)
        .Include(p => p.ExecutionCompany)
        .Include(p => p.ProjectManager)
        .Where(p => p.ProjectID == projectID)
        .Select(p => new ProjectViewModel
        {
            ProjectID = p.ProjectID,
            ProjectName = p.ProjectName,
            ClientCompanyName = p.ClientCompany.CompanyName,
            ExecutionCompanyName = p.ExecutionCompany.CompanyName,
            ProjectManagerFullName = $"{p.ProjectManager.FirstName} {p.ProjectManager.MiddleName} {p.ProjectManager.LastName}",
            StartDate = p.StartDate.ToShortDateString(),
            EndDate = p.EndDate.ToShortDateString(),
            Priority = p.Priority
        })
        .FirstOrDefault();
        
         var pe = context.ProjectEmployees;

            var employees = context.Employees
            .Include(e => e.Company)
            .Select(e => new EmployeeViewModel
            {
                EmployeeID = e.EmployeeID,
                FullName = $"{e.FirstName} {e.MiddleName} {e.LastName}",
                Company = e.Company,
                Email = e.Email,
                Role = pe
                .Where(pe => pe.ProjectID == projectID)
                .Where(pe => pe.EmployeeID == e.EmployeeID)
                .Select(pe => pe.Role)
                .FirstOrDefault(),
    
                Projects = pe
                    .Where(pe => pe.EmployeeID == e.EmployeeID)
                    .Select(pe => new EmployeeProjectsViewModel
                    {
                        ProjectID = pe.ProjectID,
                        ProjectName = pe.Project.ProjectName,
                        ClientCompanyName = pe.Project.ClientCompany.CompanyName,
                        ExecutionCompanyName = pe.Project.ExecutionCompany.CompanyName,
                        ProjectManagerFullName = $"{pe.Project.ProjectManager.FirstName} {pe.Project.ProjectManager.MiddleName} {pe.Project.ProjectManager.LastName}",
                        StartDate = pe.Project.StartDate.ToShortDateString(),
                        EndDate = pe.Project.EndDate.ToShortDateString(),
                        Priority = pe.Project.Priority,
                        Role = pe.Role
                    })
                    .ToList()
            }).ToList();

            var projectemployees = new ProjectEmployeesViewModel
            { 
                Project = project,
                Employees = employees 
            };

            return View(projectemployees);
        }



    }
}
