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
                return await database.QueryAsync<TodoListModel>("SELECT * FROM TodoList");
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

                if (existingList != null)
                {
                    
                    if (list.TodoItemsEdited)
                    {
                        list.TodoItemsEdited = false;
                       
                        foreach (TodoItemModel item in list.TodoItems)
                        {
                            await DeleteTodoItem(item);
                        }

                        foreach (TodoItemModel item in list.TodoItems)
                        {
                            await SaveTodoItem(item, list.Id);
                        }
                    }

                    await database.UpdateAsync(list);


                }
                else
                {
                    await database.InsertAsync(list);
                    await ChangeListActiveState(list);
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

        public async Task<bool> DeleteList(TodoListModel list)
        {

            try
            {
                if (list.TodoItems != null)
                {
                    foreach (TodoItemModel todoItem in list.TodoItems)
                    {
                        await database.DeleteAsync(todoItem);
                    }
                }

                await database.DeleteAsync(list);
                return true;

            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<bool> ChangeListActiveState(TodoListModel list)
        {
            try
            {

                list.Active = !list.Active;

                if (list.Active)
                {
                    var allLists = await GetAllLists();
                    foreach (TodoListModel todo in allLists)
                    {
                        todo.Active = false;
                        await SaveList(todo);
                    }
                }

                await SaveList(list);

                return true;


            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<bool> CompleteList(TodoListModel list)
        {
            try
            {
                list.Active = false;
                list.Completed = true;
                await SaveList(list);

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
            
        }
    }
}
