using System;
using System.Collections.Generic;
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
            TodoItemModel c = new TodoItemModel("C");
            Task.Run(async () => await cacheService.CacheObject("models", a, 365));

            TodoItemModel b;
            //Task.Run(async () => b = await cacheService.GetObject<TodoItemModel>("models", a.Id));
            Task.Run(async () => await cacheService.CacheObject("models", c, 365));

            List<TodoItemModel> all;
            Task.Run(async () => all = await cacheService.GetAllObjects<TodoItemModel>("models"));

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
