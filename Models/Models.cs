using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementApp.Models.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public List<Employee> Employees { get; set; } 
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; } 
    }

    public class Project
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public int ClientCompanyID { get; set; }
        public int ExecutionCompanyID { get; set; }
        public int ProjectManagerID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        public string? Priority { get; set; }

        public Company ClientCompany { get; set; }
        public Company ExecutionCompany { get; set; } 
        public Employee ProjectManager { get; set; } 
    }

    public class ProjectEmployee
    {
        public int ProjectEmployeeID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public string? Role { get; set; }

        public Project Project { get; set; } 
        public Employee Employee { get; set; } 
    }
}
