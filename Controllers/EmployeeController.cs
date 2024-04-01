using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models.ViewModels;
using ProjectManagementApp.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DBContext context;
        public EmployeeController(DBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Employee()
        {
            var employees = context.Employees
        .Include(e => e.Company)
        .Select(e => new EmployeeViewModel
        {
            EmployeeID = e.EmployeeID,
            FullName = $"{e.FirstName} {e.MiddleName} {e.LastName}",
            Company = e.Company,
            Email = e.Email
        });

            return View("Employee", employees);
        }

        public IActionResult Details(int employeeID)
        {
            var employee = context.Employees
        .Include(e => e.Company)
        .Where(e => e.EmployeeID == employeeID)
        .Select(e => new EmployeeViewModel
        {
            EmployeeID = e.EmployeeID,
            FullName = $"{e.FirstName} {e.MiddleName} {e.LastName}",
            Company = e.Company,
            Email = e.Email,
            Projects = context.ProjectEmployees
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
        }).FirstOrDefault();

            return View(employee);
        }
    }
}
