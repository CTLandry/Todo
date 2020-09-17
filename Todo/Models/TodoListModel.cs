using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;

namespace Todo.Models
{
    [Table("TodoList")]
    public class TodoListModel : _BaseModel
    {

        #region Properties

        [PrimaryKey]
        public Guid Id { get; set; }

        public bool TodoItemsEdited = false;

        private string name;
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        private List<TodoItemModel> todoItems = new List<TodoItemModel>();
        [SQLite.Ignore]
        public List<TodoItemModel> TodoItems
        {
            set
            {
                SetProperty(ref todoItems, value);
                TodoItemsEdited = true;
            }
            get { return todoItems; }
        }

        private bool active;
        public bool Active
        {
            set { SetProperty(ref active, value); }
            get { return active; }
        }

        private bool completed;
        public bool Completed
        {
            set { SetProperty(ref completed, value); }
            get { return completed; }
        }

        #endregion

        #region Constructors

        public TodoListModel()
        {
           
        }

        public TodoListModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
           
        }

        #endregion

    }
}
