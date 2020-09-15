using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Infrastructure.Exceptions;
using SQLite;
using System.IO;
using Todo.Models;

namespace Todo.Services
{

    public class CacheService : ICacheService
    {
       
        private readonly SQLiteAsyncConnection database;

        public CacheService()
        {
            try
            {
                if (database == null)
                {
                    database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Todo.db3"));
                    database.CreateTableAsync<TodoListModel>().Wait();
                    database.CreateTableAsync<TodoItemModel>().Wait();

                }
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }

        public async Task<List<TodoListModel>> GetAllLists()
        {
            try
            {
                return await database.QueryAsync<TodoListModel>("SELECT * FROM TodoList ORDER BY Active desc");
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return null;
            }
        }

        public async Task<TodoListModel> GetList(Guid listId)
        {
            try
            {
                var list = await database.QueryAsync<TodoListModel>("SELECT * FROM TodoList WHERE Id = ?", listId);
                return list[0] ?? null;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return null;
            }
        }

        public async Task<bool> SaveList(TodoListModel list)
        {
            try
            {
                var existingList = await GetList(list.Id);

                if(existingList != null)
                {
                    await database.UpdateAsync(list);
                    
                }
                else
                {
                    await database.InsertAsync(list);
                }


                foreach(TodoItemModel item in list.TodoItems)
                {
                    await SaveTodoItem(item, list.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteTodoItem(TodoItemModel todoItem)
        {
            try
            {
                await database.DeleteAsync(todoItem);
                return true;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<bool> SaveTodoItem(TodoItemModel todoItem, Guid listId)
        {
            var existingItem = await GetTodoItem(todoItem.Id);

            try
            {
                if (existingItem != null)
                {
                    await database.UpdateAsync(todoItem);

                }
                else
                {
                    todoItem.ListId = listId;
                    await database.InsertAsync(todoItem);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<TodoItemModel> GetTodoItem(Guid todoId)
        {
            try
            {
                var item = await database.QueryAsync<TodoItemModel>("SELECT * FROM TodoItem WHERE Id = ?", todoId);
                return item[0] ?? null;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return null;
            }
        }


    }
}
