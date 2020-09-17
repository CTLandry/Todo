using System;
using System.Collections.Generic;
using Todo.Controls;
using Todo.Models;
using Todo.Services;
using Todo.ViewModels;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class SaveView : Popup
    {
        #region Private Variables

        private SaveViewModel viewModel { get; set; }

        #endregion

        public SaveView(TodoListViewModel parentVM)
        {
            InitializeComponent();
            BindingContext = viewModel = new SaveViewModel(parentVM);
        }

        public SaveView(TodoListViewModel parentVM, TodoListModel todoList)
        {
            InitializeComponent();
            BindingContext = viewModel = new SaveViewModel(parentVM, todoList);
        }

        public SaveView(TodoItemViewModel parentVM, TodoListModel todoList)
        {
            InitializeComponent();
            BindingContext = viewModel = new SaveViewModel(parentVM: parentVM, todoList: todoList);
        }

        public SaveView(TodoItemViewModel parentVM, TodoListModel todoList, Guid todoItemId)
        {
            InitializeComponent();
            BindingContext = viewModel = new SaveViewModel(parentVM: parentVM, todoList: todoList, todoItemId: todoItemId);
        }
    }
}
