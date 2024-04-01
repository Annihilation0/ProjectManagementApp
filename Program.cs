using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlite("Data Source = ProjectManagementApp.db").EnableSensitiveDataLogging();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Project/Error");
    // The default HSTS value System.ArgumentException: "'AddDbContext' was called with configuration, but the context type 'DBContext' only declares a parameterless constructor. This means that the configuration passed to 'AddDbContext' will never be used. If configuration is passed to 'AddDbContext', then 'DBContext' should declare a constructor that accepts a DbContextOptions<DBContext> and must pass it to the base constructor for DbContext."is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Index}/{id?}");


app.Run();
