using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;
using Xamarin.Forms;

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

        private ICommand saveTodoItem;
        public ICommand SaveTodoItemCommand
        {
            get
            {
                return saveTodoItem ??
                new Command(async () =>
                {
                    IsBusy = true;
                    //await PopupNavigation.Instance.PushAsync(new CreateListView(this));
                    IsBusy = false;
                });
            }
        }

        private ICommand deleteTodoItem;
        public ICommand DeleteTodoItem
        {
            get
            {
                return deleteTodoItem ??
                new Command(async (item) =>
                {
                    IsBusy = true;
                    ActiveTodoList.TodoItems.Remove((TodoItemModel)item);
                    await cachingService.SaveList(ActiveTodoList);
                    IsBusy = false;
                });
            }
        }

        private ICommand completeList;
        public ICommand CompleteListCommand
        {
            get
            {
                return completeList ??
                new Command(async () =>
                {
                    IsBusy = true;
                    await cachingService.CompleteList(ActiveTodoList);
                    await navigationService.GoBackAsync();
                    IsBusy = false;
                });
            }
        }

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
