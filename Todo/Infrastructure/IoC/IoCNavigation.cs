using System;
using Prism.Ioc;
using Todo.ViewModels;
using Todo.Views;
using Xamarin.Forms;

namespace Todo.Infrastructure.IoC
{
    public static class IoCNavigation
    {
        /// <summary>
        /// Handling the registration of views to view models and other navigation items
        /// </summary>
        /// <param name="containerRegistry">Prims IoC registery</param>
        public static void RegisterViewsAndViewModels(IContainerRegistry containerRegistry)
        {
            try
            {
                containerRegistry.RegisterForNavigation<NavigationPage>();
                containerRegistry.RegisterForNavigation<TodoListView, TodoListViewModel>();
            }
            catch (Exception ex)
            {

            }
           
        }

    }
}
