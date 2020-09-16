using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using Todo.Services;
using Xamarin.Forms;

namespace Todo.ViewModels
{
    public class CreateListViewModel : _BaseViewModel
    {

        #region Member Variables

        private TodoListViewModel parentViewModel;

        #endregion

        #region Properties

        private TodoListModel todoList;
       

        private string listName = string.Empty;
        public string ListName
        {
            get { return listName; }
            set
            {
                SetProperty(ref listName, value);
                IsCreateEnabled = !string.IsNullOrWhiteSpace(value);
            }
        }

        private bool isCreateEnabled = false;
        public bool IsCreateEnabled
        {
            get { return isCreateEnabled; }
            set { SetProperty(ref isCreateEnabled, value); }
        }

        private bool isEdit = false;
        public bool IsEdit
        {
            get { return isEdit; }
            set { SetProperty(ref isEdit, value); }
        }

        #endregion

        #region Commands

        private ICommand genericCommand;
        public ICommand GenericCommand
        {
            get
            {
                return genericCommand ??
                new Command(async (action) =>
                {
                    IsBusy = true;
                    await GenericAction((string)action);
                    IsBusy = false;
                });
            }
        }

        #endregion

     
        public CreateListViewModel(TodoListViewModel parentVM)
        {
            parentViewModel = parentVM;
           
        }

        public CreateListViewModel(TodoListViewModel parentVM, TodoListModel todoList)
        {
            parentViewModel = parentVM;
            this.todoList = todoList;
            ListName = todoList.Name;
            IsEdit = true;

        }

        #region Private Methods

        private async Task GenericAction(string action)
        {
            try
            {
                switch (action)
                {
                    case "save":
                        {
                            if(IsEdit)
                            {
                                todoList.Name = ListName;
                                await parentViewModel.cachingService.SaveList(todoList);
                            }
                            else
                            {
                                await parentViewModel.cachingService.SaveList(new TodoListModel(ListName));
                            }
                            
                            await parentViewModel.RefreshTodoLists();
                            await PopupNavigation.Instance.PopAsync();
                            break;
                        }
                    case "cancel":
                        {
                            await PopupNavigation.Instance.PopAsync();
                            break;
                        }
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
