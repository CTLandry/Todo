using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public abstract class TodoViewModel : _BaseViewModel
    {

        #region Properties

        private List<TodoListModel> todoLists;
        public List<TodoListModel> TodoLists
        {
            set { SetProperty(ref todoLists, value); }
            get { return todoLists; }
        }

        #endregion

        #region Services

        public readonly ICacheService cachingService;
        public readonly INavigationService navigationService;

        #endregion

        public TodoViewModel(ICacheService cacheService, INavigationService navigationService)
        {

            try
            {
                this.cachingService = cacheService;
                this.navigationService = navigationService;
                
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #region PublicMethods

        public virtual async Task RefreshTodoLists()
        {
            try
            {
                TodoLists = await cachingService.GetAllLists();
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
               
            }
        }

        #endregion
    }
}
