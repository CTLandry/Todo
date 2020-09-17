using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;
using Todo.Views;
using Xamarin.Forms;

namespace Todo.ViewModels
{
    public class TodoListViewModel : TodoViewModel, INavigationAware
    {
        
       
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
                    await PopupNavigation.Instance.PushAsync(new SaveView(this));
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
                    await PopupNavigation.Instance.PushAsync(new SaveView(this, (TodoListModel)list));
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

        private ICommand navigateToTodoItems;

        public ICommand NavigateToTodoItemsCommand
        {
            get
            {
                return navigateToTodoItems ??
                new Command(async (list) =>
                {
                    IsBusy = true;
                    var listModel = (TodoListModel)list;
                    if(listModel.Active && !listModel.Completed)
                    {
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("todolist", list);
                        await navigationService.NavigateAsync("TodoItemView", navigationParams);
                    }
                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Constructors

        public TodoListViewModel(ICacheService cacheService, INavigationService navigationService)
            : base(cacheService, navigationService)
        {
            Task.Run(async () => await RefreshTodoLists());
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
                Task.Run(async () => await RefreshTodoLists());
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #endregion

      

    }
}
