using DepartmentHierarchyAndReminderManagementSystem.Application.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Application.Services;
using DepartmentHierarchyAndReminderManagementSystem.Domain.Interfaces;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Data;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Repositories;
using DepartmentHierarchyAndReminderManagementSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IReminderService, ReminderService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<ReminderEmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
