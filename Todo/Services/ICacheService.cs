using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface ICacheService
    {
        Task<bool> CacheObject<T>(T data)where T : IModel;
        Task<List<T>> GetAllObjects<T>(Guid ID) where T : IModel;
        Task EmptyCache();
       

    }
}
