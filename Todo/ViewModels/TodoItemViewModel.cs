using System;
using System.Collections.Generic;
using Prism.Navigation;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public class TodoItemViewModel : _BaseViewModel, INavigationAware
    {
        #region Properties

        private TodoListModel activeTodoList;
        public TodoListModel ActiveTodoList
        {
            set { SetProperty(ref activeTodoList, value); }
            get { return activeTodoList; }
        }

        #endregion

        #region Services

        public readonly ICacheService cachingService;
        public readonly INavigationService navigationService;

        #endregion

        #region Commands

        #endregion

        #region Constructors

        public TodoItemViewModel(ICacheService cachingService,
                                INavigationService navigationService)
        {
            try
            {
                this.navigationService = navigationService;
                this.cachingService = cachingService;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #endregion

        #region NavigationAware

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                ActiveTodoList = parameters.GetValue<TodoListModel>("todolist");
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods
        #endregion
    }
}
