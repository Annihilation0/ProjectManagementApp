using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "CompanyID", "Email", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, 1, "john.doe@example.com", "John", "Doe", "Smith" },
                    { 2, 2, "jane.smith@example.com", "Jane", "Smith", "Maria" },
                    { 3, 3, "alice.johnson@example.com", "Alice", "Johnson", "Smith" },
                    { 4, 1, "michael.williams@example.com", "Michael", "Williams", "Smith" },
                    { 5, 2, "sarah.brown@example.com", "Sarah", "Brown", "Smith" },
                    { 6, 3, "david.miller@example.com", "David", "Miller", "Smith" },
                    { 7, 1, "emily.anderson@example.com", "Emily", "Anderson", "Smith" },
                    { 8, 2, "ryan.wilson@example.com", "Ryan", "Wilson", "Smith" },
                    { 9, 3, "jessica.martinez@example.com", "Jessica", "Martinez", "Smith" },
                    { 10, 1, "brian.jones@example.com", "Brian", "Jones", "Smith" },
                    { 11, 2, "laura.thompson@example.com", "Laura", "Thompson", "Smith" },
                    { 12, 3, "kevin.garcia@example.com", "Kevin", "Garcia", "Smith" },
                    { 13, 1, "melissa.roberts@example.com", "Melissa", "Roberts", "Smith" },
                    { 14, 2, "christopher.lee@example.com", "Christopher", "Lee", "Smith" },
                    { 15, 3, "amanda.hernandez@example.com", "Amanda", "Hernandez", "Smith" },
                    { 16, 1, "eric.young@example.com", "Eric", "Young", "Smith" },
                    { 17, 2, "caroline.scott@example.com", "Caroline", "Scott", "Smith" },
                    { 18, 3, "adam.nelson@example.com", "Adam", "Nelson", "Smith" },
                    { 19, 1, "maria.perez@example.com", "Maria", "Perez", "Smith" },
                    { 20, 2, "derek.evans@example.com", "Derek", "Evans", "Smith" },
                    { 21, 3, "nicole.collins@example.com", "Nicole", "Collins", "Smith" },
                    { 22, 1, "patrick.wright@example.com", "Patrick", "Wright", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectID", "ClientCompanyID", "EndDate", "ExecutionCompanyID", "Priority", "ProjectManagerID", "ProjectName", "StartDate" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2023, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "high", 1, "Project 1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, new DateTime(2023, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "high", 4, "Project 2", new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 5, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "low", 6, "Project 3", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProjectEmployees",
                columns: new[] { "ProjectEmployeeID", "EmployeeID", "ProjectID", "Role" },
                values: new object[,]
                {
                    { 1, 1, 1, "Project Manager" },
                    { 2, 2, 1, "Developer" },
                    { 3, 3, 1, "QA Engineer" },
                    { 4, 4, 2, "Project Manager" },
                    { 5, 5, 2, "Designer" },
                    { 6, 6, 3, "Project Manager" },
                    { 7, 7, 3, "Developer" },
                    { 8, 8, 3, "Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProjectEmployees",
                keyColumn: "ProjectEmployeeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 6);
        }
    }
}
