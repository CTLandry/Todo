using System;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface ICacheService
    {
        Task<bool> CacheObject<T>(T model, int days) where T : _BaseModel;
        Task<T> GetAllObjects<T>();
        Task<T> GetObject<T>(string key);
        Task EmptyCache();
       

    }
}
