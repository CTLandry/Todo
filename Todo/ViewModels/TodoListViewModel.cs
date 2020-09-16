using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;
using Todo.Views;
using Xamarin.Forms;

namespace Todo.ViewModels
{
    public class TodoListViewModel : _BaseViewModel
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

        #endregion

        #region Commands

        private ICommand showCreateTodoList;
        public ICommand ShowCreateTodoListCommand
        {
            get
            {
                return showCreateTodoList ??
                new Command(async (list) =>
                {
                    IsBusy = true;
                    await PopupNavigation.Instance.PushAsync(new CreateListView(this));
                    IsBusy = false;
                });
            }
        }

        private ICommand editTodoList;
        public ICommand EditTodoListCommand
        {
            get
            {
                return editTodoList ??
                new Command(async (list) =>
                {
                    IsBusy = true;
                    await PopupNavigation.Instance.PushAsync(new CreateListView(this, (TodoListModel)list));
                    IsBusy = false;
                });
            }
        }

        private ICommand deleteTodoList;
        public ICommand DeleteTodoList
        {
            get
            {
                return deleteTodoList ??
                new Command(async (list) =>
                {
                IsBusy = true;
                await cachingService.DeleteList((TodoListModel)list);
                await RefreshTodoLists();
                IsBusy = false;
                });
            }
        }

        private ICommand changeActiveState;
        public ICommand ChangeActiveStateCommand
        {
            get
            {
                return changeActiveState ??
                new Command(async (list) =>
                {
                    IsBusy = true;
                    await cachingService.ChangeListActiveState((TodoListModel)list);
                    await RefreshTodoLists();
                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Constructors

        public TodoListViewModel(ICacheService cacheService)
        {
            
            try
            {
                this.cachingService = cacheService;
                Task.Run(async () => await RefreshTodoLists());

            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #endregion

        #region PublicMethods

        public async Task RefreshTodoLists()
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

        #region PrivateMethods



        #endregion

    }
}
