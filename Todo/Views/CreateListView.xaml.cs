using System;
using System.Collections.Generic;
using Todo.Controls;
using Todo.Models;
using Todo.Services;
using Todo.ViewModels;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class CreateListView : Popup
    {
        #region Private Variables

        private CreateListViewModel viewModel { get; set; }

        #endregion

        public CreateListView(TodoListViewModel parentVM, TodoListModel todoList)
        {
            InitializeComponent();
            BindingContext = viewModel = new CreateListViewModel(parentVM, todoList);
        }
    }
}
