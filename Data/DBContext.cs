using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Models;

namespace ProjectManagementApp.Data
{
    public class DBContext : DbContext
    {

        public  DbSet<Company> Companies { get; set; }
        public  DbSet<Employee> Employees { get; set; }
        public  DbSet<Project> Projects { get; set; }
        public  DbSet<ProjectEmployee> ProjectEmployees { get; set; }


        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(c => c.CompanyID);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeID);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectID);
            modelBuilder.Entity<ProjectEmployee>().HasKey(pe => pe.ProjectEmployeeID);


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.CompanyID);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ClientCompany)
                .WithMany()
                .HasForeignKey(p => p.ClientCompanyID);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ExecutionCompany)
                .WithMany()
                .HasForeignKey(p => p.ExecutionCompanyID);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManager)
                .WithMany()
                .HasForeignKey(p => p.ProjectManagerID);

            modelBuilder.Entity<ProjectEmployee>()
                 .HasOne(pe => pe.Project)
                 .WithMany()
                 .HasForeignKey(pe => pe.ProjectID);

            modelBuilder.Entity<ProjectEmployee>()
                 .HasOne(pe => pe.Employee)
                 .WithMany()
                 .HasForeignKey(pe => pe.EmployeeID);

            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyID = 1, CompanyName = "Company A" },
                new Company { CompanyID = 2, CompanyName = "Company B" },
                new Company { CompanyID = 3, CompanyName = "Company C" },
                new Company { CompanyID = 4, CompanyName = "Company D" },
                new Company { CompanyID = 5, CompanyName = "Company E" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe", MiddleName = "Smith", Email = "john.doe@example.com", CompanyID = 1},
                new Employee {EmployeeID = 2, FirstName = "Jane", LastName = "Smith", MiddleName = "Maria", Email = "jane.smith@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 3, FirstName = "Alice", LastName = "Johnson", MiddleName = "Smith", Email = "alice.johnson@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 4, FirstName = "Michael", LastName = "Williams", MiddleName = "Smith", Email = "michael.williams@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 5, FirstName = "Sarah", LastName = "Brown", MiddleName = "Smith", Email = "sarah.brown@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 6, FirstName = "David", LastName = "Miller", MiddleName = "Smith", Email = "david.miller@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 7, FirstName = "Emily", LastName = "Anderson", MiddleName = "Smith", Email = "emily.anderson@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 8, FirstName = "Ryan", LastName = "Wilson", MiddleName = "Smith", Email = "ryan.wilson@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 9,  FirstName = "Jessica", LastName = "Martinez", MiddleName = "Smith", Email = "jessica.martinez@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 10, FirstName = "Brian", LastName = "Jones", MiddleName = "Smith", Email = "brian.jones@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 11, FirstName = "Laura", LastName = "Thompson", MiddleName = "Smith", Email = "laura.thompson@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 12, FirstName = "Kevin", LastName = "Garcia", MiddleName = "Smith", Email = "kevin.garcia@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 13, FirstName = "Melissa", LastName = "Roberts", MiddleName = "Smith", Email = "melissa.roberts@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 14, FirstName = "Christopher", LastName = "Lee", MiddleName = "Smith", Email = "christopher.lee@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 15, FirstName = "Amanda", LastName = "Hernandez", MiddleName = "Smith", Email = "amanda.hernandez@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 16, FirstName = "Eric", LastName = "Young", MiddleName = "Smith", Email = "eric.young@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 17, FirstName = "Caroline", LastName = "Scott", MiddleName = "Smith", Email = "caroline.scott@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 18, FirstName = "Adam", LastName = "Nelson", MiddleName = "Smith", Email = "adam.nelson@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 19, FirstName = "Maria", LastName = "Perez", MiddleName = "Smith", Email = "maria.perez@example.com", CompanyID = 1 },
                new Employee {EmployeeID = 20, FirstName = "Derek", LastName = "Evans", MiddleName = "Smith", Email = "derek.evans@example.com", CompanyID = 2 },
                new Employee {EmployeeID = 21, FirstName = "Nicole", LastName = "Collins", MiddleName = "Smith", Email = "nicole.collins@example.com", CompanyID = 3 },
                new Employee {EmployeeID = 22, FirstName = "Patrick", LastName = "Wright", MiddleName = "Smith", Email = "patrick.wright@example.com", CompanyID = 1 }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectID = 1, ProjectName = "Project 1", ClientCompanyID = 4, ExecutionCompanyID = 1, ProjectManagerID = 1, StartDate = DateTime.Parse("2023-01-01"), EndDate = DateTime.Parse("2023-02-28"), Priority = "high" },
                new Project { ProjectID = 2, ProjectName = "Project 2", ClientCompanyID = 4, ExecutionCompanyID = 2, ProjectManagerID = 4, StartDate = DateTime.Parse("2023-03-01"), EndDate = DateTime.Parse("2023-04-30"), Priority = "high" },
                new Project { ProjectID = 3, ProjectName = "Project 3", ClientCompanyID = 5, ExecutionCompanyID = 3, ProjectManagerID = 6, StartDate = DateTime.Parse("2023-05-01"), EndDate = DateTime.Parse("2023-06-30"), Priority = "low" }
            );

            modelBuilder.Entity<ProjectEmployee>().HasData(
                new ProjectEmployee { ProjectEmployeeID = 1, ProjectID = 1, EmployeeID = 1, Role = "Project Manager" },
                new ProjectEmployee { ProjectEmployeeID = 2, ProjectID = 1, EmployeeID = 2, Role = "Developer" },
                new ProjectEmployee { ProjectEmployeeID = 3, ProjectID = 1, EmployeeID = 3, Role = "QA Engineer" },
                new ProjectEmployee { ProjectEmployeeID = 4, ProjectID = 2, EmployeeID = 4, Role = "Project Manager" },
                new ProjectEmployee { ProjectEmployeeID = 5, ProjectID = 2, EmployeeID = 5, Role = "Designer" },
                new ProjectEmployee { ProjectEmployeeID = 6, ProjectID = 3, EmployeeID = 6, Role = "Project Manager" },
                new ProjectEmployee { ProjectEmployeeID = 7, ProjectID = 3, EmployeeID = 7, Role = "Developer" },
                new ProjectEmployee { ProjectEmployeeID = 8, ProjectID = 3, EmployeeID = 8, Role = "Developer" }
            );

        
        }
    }
}
