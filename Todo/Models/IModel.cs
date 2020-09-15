using System;
namespace Todo.Models
{
    public interface IModel
    {
          Guid PrimaryKey { get; }
    }
}
