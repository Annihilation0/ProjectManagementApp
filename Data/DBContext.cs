using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
                .WithMany(c => c.Employees)
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
        }
    }

    public class Company
    {
        public  int CompanyID { get; set; }
        public  string? CompanyName { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    public class Employee
    {
        public  int EmployeeID { get; set; }
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public  string? MiddleName { get; set; }
        public  string? Email { get; set; }
        public  int CompanyID { get; set; }

        public Company Company { get; set; } = new Company();
    }

    public class Project
    {
        public  int ProjectID { get; set; }
        public  string? ProjectName { get; set; }
        public int ClientCompanyID { get; set; }
        public  int ExecutionCompanyID { get; set; }
        public int ProjectManagerID { get; set; }
        // явно указываем тип столбца, потому что EF Core не преобразует тип DateTime 
        [Column(TypeName = "datetime")]
        public  DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public  DateTime EndDate { get; set; }
        public  string? Priority { get; set; }

        public Company ClientCompany { get; set; } = new Company();
        public Company ExecutionCompany { get; set; } = new Company();
        public Employee ProjectManager { get; set; } = new Employee();
    }

    public class ProjectEmployee
    {
        public int ProjectEmployeeID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public  string? Role { get; set; }

        public Project Project { get; set; } = new Project();   
        public Employee Employee { get; set; } = new Employee();
    }
}
