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
        //Display information about all employees
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
        //Display employee information
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
        //Deleting an employee
        public IActionResult DeleteEmployee(int employeeID)
        {
            var projectEmployees = context.ProjectEmployees.Where(pe => pe.EmployeeID == employeeID).ToList();
            context.ProjectEmployees.RemoveRange(projectEmployees);

            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == employeeID);
            if (employee != null)
            {
                context.Employees.Remove(employee);
            }

            context.SaveChanges();
            return RedirectToAction("Employee");
        }
        //Display form for adding a new employee
        public IActionResult WizardEmployeeInfo()
        {
            EmployeeCompaniesViewModel ec = new EmployeeCompaniesViewModel();
            var companies = context.Companies;
            ec.Companies = companies.ToList();
            return View(ec);
        }
        // Adding a new employee
        public IActionResult AddEmployee(EmployeeCompaniesViewModel ec)
        {

            int employeeID = context.Employees.Max(e => e.EmployeeID) + 1;
            string firstName = ec.Employee.FirstName ?? "";
            string lastName = ec.Employee.LastName ?? "";
            string middleName = ec.Employee.MiddleName ?? "";
            string email = ec.Employee.Email ?? "";
            int companyID = ec.Employee.CompanyID;

            var newEmployee = new Employee
            {
               EmployeeID = employeeID,
               FirstName = firstName,
               LastName = lastName,
               MiddleName = middleName,
               Email = email,
               CompanyID = companyID
            };
            
            context.Employees.Add(newEmployee);
            context.SaveChanges();

            return RedirectToAction("Employee");
        }
    }
}
