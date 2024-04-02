using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models.ViewModels;
using ProjectManagementApp.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DBContext context;

        public CompanyController(DBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Display information about all companies
        public IActionResult Companies()
        {
            var companies = context.Companies
            .Select(c => new Company
            {
                CompanyID = c.CompanyID,
                CompanyName = c.CompanyName
            });

            return View(companies);
        }
        //Display company information
        public IActionResult Details(int companyID)
        {
            var companies = context.Companies
            .Where(c => c.CompanyID == companyID)
            .Select(c => new Company
            {
                CompanyID = c.CompanyID,
                CompanyName = c.CompanyName,
                Employees = c.Employees
            })
            .FirstOrDefault();

            return View(companies);
        }
    }
}
