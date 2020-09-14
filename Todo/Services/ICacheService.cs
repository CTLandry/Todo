using System;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface ICacheService
    {
        Task<bool> CacheObject<T>(string serviceName, T model, int days) where T : _BaseModel;
        Task<T> GetObject<T>(string serviceName, Guid modelId) where T : _BaseModel;
        Task<T> GetAllObjects<T>(string serviceName) where T : _BaseModel;
        Task EmptyCache();
       

    }
}
