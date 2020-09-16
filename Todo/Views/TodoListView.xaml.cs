using System;
using System.Collections.Generic;
using Todo.Infrastructure.Exceptions;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class TodoListView : ContentPage
    {
        public TodoListView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
           
        }
    }
}
