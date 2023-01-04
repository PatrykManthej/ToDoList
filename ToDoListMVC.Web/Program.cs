using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoListMVC.Application;
using ToDoListMVC.Application.ViewModels.ToDoList;
using ToDoListMVC.Application.ViewModels.ToDoTask;
using ToDoListMVC.Infrastructure;
using ToDoListMVC.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IValidator<ToDoListVm>, ToDoListVmValidator>();
builder.Services.AddTransient<IValidator<ToDoTaskVm>, ToDoTaskVmValidator>();

builder.Services.AddHostedService<MailingWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ToDoList/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{id?}");

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<Context>();

var pendingMigrations = dbContext!.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}
app.Run();
