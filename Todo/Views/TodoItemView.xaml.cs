using System;
using System.Collections.Generic;
using Todo.Infrastructure.Exceptions;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class TodoItemView : ContentPage
    {
        public TodoItemView()
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
