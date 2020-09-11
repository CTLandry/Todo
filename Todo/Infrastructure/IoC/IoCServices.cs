using System;
using Prism.Ioc;
using Todo.Services;

namespace Todo.Infrastructure.IoC
{
    public static class IoCServices
    {
        public static void RegisterServices(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterInstance<IConfiguration>(config);
            containerRegistry.RegisterSingleton<ITodoService, TodoService>();
            //containerRegistry.RegisterSingleton<ICache, CachingService>();
        }
    }
}
