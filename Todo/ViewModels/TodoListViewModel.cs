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

            TodoListModel list1 = new TodoListModel("List1");

            TodoItemModel a = new TodoItemModel("A");
            TodoItemModel b = new TodoItemModel("B");
            TodoItemModel c = new TodoItemModel("C");

            list1.TodoItems.Add(a);
            list1.TodoItems.Add(b);

            cacheService.SaveList(list1);

            List<TodoListModel> list2 = null;
            Task.Run(async () => list2 = await cacheService.GetAllLists());

            Debug.WriteLine("Test");
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
