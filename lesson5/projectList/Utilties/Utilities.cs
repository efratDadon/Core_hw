using projectList.Interfaces;
using projectList.Services;

namespace projectList.Utilities;

public static class Utilities
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<taskItem, taskService>();
    }
}