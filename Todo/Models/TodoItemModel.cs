using System;
namespace Todo.Models
{
    public class TodoItemModel : _BaseModel
    {
       
        #region Properties

        private string name;
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        private string description;
        public string Description
        {
            set { SetProperty(ref description, value); }
            get { return description; }
        }

        private bool completed = false;
        public bool Completed
        {
            set { SetProperty(ref completed, value); }
            get { return completed; }
        }

        #endregion

        #region Constructors

        public TodoItemModel(string name)
        {
            Name = name;
        }

        public TodoItemModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion
    }
}
