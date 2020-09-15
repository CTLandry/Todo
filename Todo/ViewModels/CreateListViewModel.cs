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
        public TodoListModel TodoList
        {
            get { return todoList; }
            set
            {
                SetProperty(ref todoList, value);
                IsCreateEnabled = !string.IsNullOrWhiteSpace(value.Name);
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

     
        public CreateListViewModel(TodoListViewModel parentVM, TodoListModel todoList)
        {
            parentViewModel = parentVM;
           
            TodoList = todoList;
        }

        #region Private Methods

        private async Task GenericAction(string action)
        {
            try
            {
                switch (action)
                {
                    case "create":
                        {
                            await parentViewModel.cachingService.SaveList(TodoList);
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
