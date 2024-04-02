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
        //Display information about all projects
        public IActionResult Index()
        {

            var projects = GetAllProjects();
            return View("Projects", projects);

        }
        //Display project information
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
        .First();
        
         var pe = context.ProjectEmployees;

            var employees = pe
            .Where(pe => pe.ProjectID == projectID)
            .Select(pe => new EmployeeViewModel
            {
                EmployeeID = pe.EmployeeID,
                FullName = $"{pe.Employee.FirstName} {pe.Employee.MiddleName} {pe.Employee.LastName}",
                Company = pe.Employee.Company,
                Email = pe.Employee.Email,
                Role = pe.Role
            }).ToList();

            var projectemployees = new ProjectEmployeesViewModel
            { 
                Project = project,
                Employees = employees 
            };

            return View(projectemployees);
        }
        //Display form for adding a new project - step 1st
        public IActionResult WizardProjectInfo()
        {
            var wizard = new WizardViewModel();
            return View(wizard);
        }
        //Display form for adding a new project - step 2nd
        public IActionResult WizardCompanyChoice(WizardViewModel wizard)
        {
            if (!TempData.ContainsKey("ProjectName"))
            {
                TempData["ProjectName"] = wizard.Project.ProjectName;
                TempData["StartDate"] = wizard.Project.StartDate.ToLongDateString();
                TempData["EndDate"] = wizard.Project.EndDate.ToShortDateString();
                TempData["Priority"] = wizard.Project.Priority;
            }

            var companies = context.Companies.ToList();
            wizard.AllCompanies = companies;

            return View(wizard);
        }
        //Display form for adding a new project - step 3rd
        public IActionResult WizardManagerChoice(WizardViewModel wizard)
        {
            int executor = 0;
            if (!TempData.ContainsKey("ClientCompanyID"))
            {
                TempData["ClientCompanyID"] = wizard.Project.ClientCompany.CompanyID;
                TempData["ExecutionCompanyID"] = wizard.Project.ExecutionCompany.CompanyID;
                executor = wizard.Project.ExecutionCompany.CompanyID;
            }
            executor = Convert.ToInt32(TempData.Peek("ExecutionCompanyID"));
            var employees = context.Employees
            .Where(e => e.CompanyID == executor)
            .Select(e => new EmployeeViewModel
            {
                EmployeeID = e.EmployeeID,
                FullName = $"{e.FirstName} {e.MiddleName} {e.LastName}",
                Company = e.Company,
                Email = e.Email
            }).ToList();
            wizard.CompanyEmployees = employees;

            return View(wizard);
        }
        //Adding a new project
        public IActionResult AddProject(WizardViewModel wizard)
        {

            int projectID = context.Projects.Max(e => e.ProjectID) + 1;
            var projectName = TempData.Peek("ProjectName") ?? ("Project_"+ projectID);
            int clientCompanyID = Convert.ToInt32(TempData.Peek("ClientCompanyID"));
            int executionCompanyID = Convert.ToInt32(TempData.Peek("ExecutionCompanyID"));
            int projectManagerID = wizard.Project.ProjectManagerID;
            DateTime startDate = Convert.ToDateTime(TempData.Peek("StartDate") ?? DateTime.Today);
            DateTime endDate = Convert.ToDateTime(TempData.Peek("EndDate") ?? DateTime.Today.AddDays(1));
            var priority = TempData.Peek("Priority") ?? "medium";

            var newProject = new Project
            {
                ProjectID = projectID, 
                ProjectName = projectName.ToString(),
                ClientCompanyID = clientCompanyID,
                ExecutionCompanyID = executionCompanyID,
                ProjectManagerID = projectManagerID,
                StartDate = startDate,
                EndDate = endDate,
                Priority = priority.ToString()
            };
            var newProjectEmployee = new ProjectEmployee
            {
                ProjectEmployeeID = context.ProjectEmployees.Max(e => e.ProjectEmployeeID) + 1,
                ProjectID = projectID,
                EmployeeID = projectManagerID,
                Role = "Project Manager"
            };
            context.Projects.Add(newProject);
            context.ProjectEmployees.Add(newProjectEmployee);
            context.SaveChanges();

            var companyID = Convert.ToInt32(TempData.Peek("ExecutionCompanyID"));

            FillEmployees(projectID, companyID, wizard);

            wizard.Project = newProject;
            return View("WizardEmployeesChoice", wizard);
        }
        //Display form for adding a new project - step 4th
        public IActionResult WizardEmployeesChoice(WizardViewModel wizard)
        {
            int projectID = context.Projects.Max(e => e.ProjectID);
            int executionCompanyID = Convert.ToInt32(TempData.Peek("ExecutionCompanyID"));
            int projectManagerID = wizard.Project.ProjectManagerID;
            var companyID = Convert.ToInt32(TempData.Peek("ExecutionCompanyID"));

            FillEmployees(projectID, companyID, wizard);

            return View(wizard);
        }
        //Adding a new project employee
        public IActionResult WizardAddEmployeeToProject(WizardViewModel wizard)
        {
            var projectID = context.Projects.Max(e => e.ProjectID);
            wizard.Project = context.Projects.First(p => p.ProjectID == projectID);
            var companyID = Convert.ToInt32(TempData["ExecutionCompanyID"]);
            var projectManagerID = Convert.ToInt32(TempData["ProjectManagerID"]);
            int employeeID = wizard.SelectedEmplyee.EmployeeID;
            int projectemployeeID = context.ProjectEmployees.Max(e => e.ProjectEmployeeID) + 1;
            string role = wizard.SelectedEmplyee.Role ?? "Developer";

            var existingProjectEmployee = context.ProjectEmployees
                .FirstOrDefault(pe => pe.EmployeeID == employeeID && pe.ProjectID == projectID && pe.Role == role);


            var newProjectEmployee = new ProjectEmployee
            {
                ProjectEmployeeID = projectemployeeID,
                EmployeeID = employeeID,
                ProjectID = projectID,
                Role = role
            };
            if (existingProjectEmployee != null)
            {
                ModelState.AddModelError("", "Сотрудник уже существует.");
            }
            else
            {
                context.ProjectEmployees.Add(newProjectEmployee);
                context.SaveChanges();
            }

            FillEmployees(projectID, companyID, wizard);

            return View("WizardEmployeesChoice", wizard);
        }
        //Display of company/project employees
        public void FillEmployees(int projectID, int companyID, WizardViewModel wizard)
        {
            var companyEmployees = context.Employees
            .Where(e => e.CompanyID == companyID)
            .Select(e => new EmployeeViewModel
            {
                EmployeeID = e.EmployeeID,
                FullName = $"{e.FirstName} {e.MiddleName} {e.LastName}",
                Company = e.Company,
                Email = e.Email
            }).ToList();

            var projectEmployees = context.ProjectEmployees
            .Where(pe => pe.ProjectID == projectID)
            .Select(pe => new EmployeeViewModel
            {
                EmployeeID = pe.EmployeeID,
                FullName = $"{pe.Employee.FirstName} {pe.Employee.MiddleName} {pe.Employee.LastName}",
                Company = pe.Employee.Company,
                Email = pe.Employee.Email,
                Role = pe.Role
            }).ToList();

            wizard.CompanyEmployees = companyEmployees;
            wizard.ProjectEmployees = projectEmployees;
        }
        //Deleting a project
        public IActionResult DeleteProject(int projectID)
        {
            var projectEmployees = context.ProjectEmployees.Where(pe => pe.ProjectID == projectID).ToList();
            context.ProjectEmployees.RemoveRange(projectEmployees);

            var project = context.Projects.FirstOrDefault(p => p.ProjectID == projectID);
            if (project != null)
            {
                context.Projects.Remove(project);
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Retrieving all projects from the context 
        public IQueryable<ProjectViewModel> GetAllProjects()
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

            return projects;
        }
        

    }
}
