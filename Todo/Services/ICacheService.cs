using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface ICacheService
    {
        
        Task<List<TodoListModel>> GetAllLists();
        Task<TodoListModel> GetList(Guid listId);
        Task<bool> DeleteList(TodoListModel list);
        Task<bool> ChangeListActiveState(TodoListModel list);
        Task<bool> SaveList(TodoListModel list);
        Task<bool> CompleteList(TodoListModel list);

        Task<bool> DeleteTodoItem(TodoItemModel todoItem);
        Task<bool> SaveTodoItem(TodoItemModel todoItem, Guid listId);
        Task<TodoItemModel> GetTodoItem(Guid todoId);
    }
}
