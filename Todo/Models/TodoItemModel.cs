using System;
using SQLite;

namespace Todo.Models
{
    [Table("TodoItem")]
    public class TodoItemModel : _BaseModel
    {

        #region Properties

        [PrimaryKey]
        public Guid Id { get; set; }

        [NotNull]
        public Guid ListId { get; set; }

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

        public TodoItemModel()
        {
           
        }
        public TodoItemModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            
        }

        public TodoItemModel(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            
        }

        #endregion
    }
}
