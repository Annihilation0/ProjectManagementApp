using ProjectManagementApp.Models.Models;

namespace ProjectManagementApp.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ClientCompanyName { get; set; }
        public string? ExecutionCompanyName { get; set; }
        public string? ProjectManagerFullName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Priority { get; set; }
        public List<ProjectEmployee> ProjectEmployees { get; set; } 
    }
    public class EmployeeProjectsViewModel
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ClientCompanyName { get; set; }
        public string? ExecutionCompanyName { get; set; }
        public string? ProjectManagerFullName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Priority { get; set; }
        public string? Role { get; set; }
    }
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public Company Company { get; set; }
        public List<EmployeeProjectsViewModel>? Projects{ get; set; }
    }
    public class ProjectEmployeesViewModel
    {
        public ProjectViewModel Project {  get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }

    public class CompanyEmployeesViewModel
    {
        public Company Company { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }
    public class EmployeeCompaniesViewModel
    {
        public List<Company> Companies { get; set; }
        public Employee Employee { get; set; }
    }

    public class WizardViewModel
    {
        public Project Project { get; set; }
        public EmployeeViewModel SelectedEmplyee { get; set; }
        public List<EmployeeViewModel> ProjectEmployees { get; set; }
        public List<EmployeeViewModel> CompanyEmployees { get; set; }
        public List<Company> AllCompanies { get; set; }
    }
    

}
