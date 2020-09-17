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
                var lists = await database.QueryAsync<TodoListModel>("SELECT * FROM TodoList");

                foreach(TodoListModel list in lists)
                {
                    var todoItems = await GetTodoItems(list.Id);

                    if(todoItems != null && todoItems.Count > 0)
                    {
                        list.TodoItems = new List<TodoItemModel>(todoItems);
                    }
                    else
                    {
                        list.TodoItems = new List<TodoItemModel>();
                    }
                   
                }

                return lists;
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

                if(list != null && list.Count > 0)
                {
                    var todoItems = await GetTodoItems(list[0].Id);

                    if (todoItems != null && todoItems.Count > 0)
                    {
                        list[0].TodoItems = new List<TodoItemModel>(todoItems);
                    }
                    else
                    {
                        list[0].TodoItems = new List<TodoItemModel>();
                    }
                    var listTodoItems = await GetTodoItems(list[0].Id);

                    return list[0];
                }

                return null;
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

        public async Task<List<TodoItemModel>> GetTodoItems(Guid todoListId)
        {
            try
            {
                var todoItems = await database.QueryAsync<TodoItemModel>($"SELECT * FROM TodoItem where ListId = ?", todoListId);
                return todoItems ?? null;
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
                    var allLists = await database.QueryAsync<TodoListModel>("SELECT * FROM TodoList");
                    foreach (TodoListModel todolist in allLists)
                    {
                        todolist.Active = false;
                        await database.UpdateAsync(todolist);
                    }
                }

                await database.UpdateAsync(list);

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
