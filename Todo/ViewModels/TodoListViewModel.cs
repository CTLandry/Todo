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

            var todoList = new TodoListModel("Test List");
            var result = Task.Run(async () => await cachingService.CacheObject(todoList, 365));
           
        }

        #endregion

        #region PublicMethods



        #endregion

        #region PrivateMethods



        #endregion

    }
}
