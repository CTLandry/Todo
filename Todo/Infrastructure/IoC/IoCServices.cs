using System;
using Prism.Ioc;
using Todo.Infrastructure.Exceptions;
using Todo.Infrastructure.Globals;
using Todo.Services;

namespace Todo.Infrastructure.IoC
{
    public static class IoCServices
    {
        public static void RegisterServices(IContainerRegistry containerRegistry)
        {
            try
            {
                containerRegistry.RegisterInstance<AppModel>(new AppModel());
                containerRegistry.RegisterSingleton<ITodoService, TodoService>();
                //containerRegistry.RegisterSingleton<ICache, CachingService>();
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
            
        }
    }
}
