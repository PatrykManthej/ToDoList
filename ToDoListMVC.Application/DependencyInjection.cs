using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.Services;

namespace ToDoListMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IToDoListService, ToDoListService>();
            services.AddTransient<IToDoTaskService, ToDoTaskService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
