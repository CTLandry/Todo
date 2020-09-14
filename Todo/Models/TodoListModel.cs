using System;
using System.Collections.ObjectModel;

namespace Todo.Models
{
   
    public class TodoListModel : _BaseModel
    {
        
        #region Properties

        private string name;
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        private ObservableCollection<TodoItemModel> todoItems;
        public ObservableCollection<TodoItemModel> TodoItems
        {
            set { SetProperty(ref todoItems, value); }
            get { return todoItems; }
        }

        private bool active;
        public bool Active
        {
            set { SetProperty(ref active, value); }
            get { return active; }
        }

        #endregion

        #region Constructors

        public TodoListModel(string name)
        {
            Name = name;
        }

        #endregion

    }
}
