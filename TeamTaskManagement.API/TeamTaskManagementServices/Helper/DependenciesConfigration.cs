using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TeamTaskManagement.Services.Chat;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Services;
using TeamTaskManagement.Services.Validators;

namespace TeamTaskManagement.Services.Helper.Configuration
{
    public static class DependenciesConfigration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ILookupItemsService, LookupItemsServices>();
            services.AddTransient<IValidator<TaskModel>, TaskItemValidator>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddSingleton<IChatService, ChatServices>();
        }

    }
}



