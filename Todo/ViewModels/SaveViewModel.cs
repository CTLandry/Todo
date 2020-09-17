using System;
using System.Linq;
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
    public class SaveViewModel : _BaseViewModel
    {

        #region Member Variables

        private readonly TodoViewModel parentViewModel;
       
        #endregion

        #region Properties

        private TodoListModel todoList;
        private Guid todoItemEditId;
        

        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
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

        private bool isTodoItemSave = false;
        public bool IsTodoItemSave
        {
            get { return isTodoItemSave; }
            set { SetProperty(ref isTodoItemSave, value); }
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

     
        public SaveViewModel(TodoListViewModel parentVM)
        {
            parentViewModel = parentVM;
            IsEdit = false;
        }

        public SaveViewModel(TodoListViewModel parentVM, TodoListModel todoList)
        {
            parentViewModel = parentVM;
            this.todoList = todoList;
            Name = todoList.Name;
            IsEdit = true;
        }

        public SaveViewModel(TodoItemViewModel parentVM, TodoListModel todoList)
        {
            parentViewModel = parentVM;
            this.todoList = todoList;
            IsTodoItemSave = true;
            IsEdit = false;

        }

        public SaveViewModel(TodoItemViewModel parentVM, TodoListModel todoList, Guid todoItemId)
        {
            parentViewModel = parentVM;
            this.todoList = todoList;
            this.todoItemEditId = todoItemId;
            Name = todoList.TodoItems.Where(item => item.Id == todoItemEditId).FirstOrDefault().Name;
            IsEdit = true;
            IsTodoItemSave = true;
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
                            if(IsEdit && IsTodoItemSave)
                            {
                                todoList.TodoItems.Where(item => item.Id == todoItemEditId).FirstOrDefault().Name = Name;
                                await parentViewModel.cachingService.SaveList(todoList);
                               
                            }
                            else if(IsTodoItemSave)
                            {
                                todoList.TodoItems.Add(new TodoItemModel(Name));
                                await parentViewModel.cachingService.SaveList(todoList);
                                
                            }
                            else if(IsEdit)
                            {
                                todoList.Name = Name;
                                await parentViewModel.cachingService.SaveList(todoList);
                               
                            }
                            else
                            {
                                await parentViewModel.cachingService.SaveList(new TodoListModel(Name));
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
