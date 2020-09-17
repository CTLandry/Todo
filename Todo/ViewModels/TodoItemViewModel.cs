using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;
using Todo.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Todo.ViewModels
{
    public class TodoItemViewModel : TodoViewModel, INavigationAware
    {

        #region Properties

        private bool isEmpty = false;
        public bool IsEmpty
        {
            set { SetProperty(ref isEmpty, value); }
            get { return isEmpty; }
        }

        private TodoListModel todoList;
        public TodoListModel TodoList
        {
            set { SetProperty(ref todoList, value); }
            get { return todoList; }
        }

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
                    await PopupNavigation.Instance.PushAsync(new SaveView(parentVM: this, todoList));
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
                    await cachingService.DeleteTodoItem((TodoItemModel)item);
                    await RefreshTodoLists();
                    IsBusy = false;
                });
            }
        }

        private ICommand editTodoItem;
        public ICommand EditTodoItemCommand
        {
            get
            {
                return editTodoItem ??
                new Command(async (item) =>
                {
                    IsBusy = true;
                    var todoItem = (TodoItemModel)item;
                    await PopupNavigation.Instance.PushAsync(new SaveView(this, TodoList, todoItem.Id));
                    IsBusy = false;
                });
            }
        }

        private ICommand todoChecked;
        public ICommand TodoCheckedCommand
        {
            get
            {
                return todoChecked ??
                new Command(async (item) =>
                {
                    IsBusy = true;
                    var todoItem = (TodoItemModel)item;
                    todoItem.Checked = !todoItem.Checked;
                    await cachingService.SaveTodoItem(todoItem, TodoList.Id);
                    await RefreshTodoLists();
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
                    await cachingService.CompleteList(TodoList);
                    await navigationService.GoBackAsync();
                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Constructors

        public TodoItemViewModel(ICacheService cacheService, INavigationService navigationService)
            : base(cacheService, navigationService)
        {
           
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
                TodoList = parameters.GetValue<TodoListModel>("todolist");
                Task.Run(async () => await RefreshTodoLists());
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        #endregion

        #region Public Methods

        public override async Task RefreshTodoLists()
        {
            try
            {
                if(TodoList != null)
                {
                   
                    TodoList = await cachingService.GetList(TodoList.Id);
                    IsEmpty = TodoList.TodoItems == null || TodoList.TodoItems.Count <= 0;
                }
                else
                {
                    IsEmpty = true;
                }
               
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);

            }
        }

        #endregion



    }
}
