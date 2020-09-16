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

        private bool isChecked = false;
        public bool Checked
        {
            set { SetProperty(ref isChecked, value); }
            get { return isChecked; }
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

        #endregion
    }
}
