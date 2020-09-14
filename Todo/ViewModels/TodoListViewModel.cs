using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public class TodoListViewModel : _BaseViewModel
    {
        #region Properties

        private ObservableCollection<TodoListModel> todoLists;
        public ObservableCollection<TodoListModel> TodoLists
        {
            set { SetProperty(ref todoLists, value); }
            get { return todoLists; }
        }

        #endregion

        #region Services

        ICacheService cachingService;

        #endregion

        #region Commands




        #endregion

        #region Constructors

        public TodoListViewModel(ICacheService cacheService)
        {
            this.cachingService = cacheService;

            TodoItemModel a = new TodoItemModel("A");
            Task.Run(async () => await cacheService.CacheObject<TodoItemModel>(a, 365));
            TodoItemModel b;
            Task.Run(async () => await cacheService.GetObject<TodoItemModel>(a.Id.ToString()));

        }

        #endregion

        #region PublicMethods



        #endregion

        #region PrivateMethods

        private async Task SaveTodoList(TodoListModel todoList)
        {

        }

        #endregion

    }
}
