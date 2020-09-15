using System;
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

        private string name;
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        private ObservableCollection<TodoItemModel> todoItems;
        [SQLite.Ignore]
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

        public TodoListModel()
        {
           
        }

        public TodoListModel(bool create)
        {
            Id = Guid.NewGuid();
            TodoItems = new ObservableCollection<TodoItemModel>();
        }

        #endregion

    }
}
