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

        private ObservableCollection<TodoListModel> todoLists;
        public ObservableCollection<TodoListModel> TodoLists
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
                    await PopupNavigation.Instance.PushAsync(new CreateListView(this, new TodoListModel(true)));
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
