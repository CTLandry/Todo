using System;
using System.Threading.Tasks;
using MonkeyCache;
using MonkeyCache.FileStore;
using Todo.Infrastructure.Exceptions;
using Todo.Models;

namespace Todo.Services
{
    /// <summary>
    /// Implementation of Monkey Cache
    /// <see href="https://github.com/jamesmontemagno/monkey-cache"/>
    /// </summary>
    public class CacheService : ICacheService
    {

        private const string BarrelId = "com.vts.todobarrel";

        public CacheService()
        {
            Barrel.ApplicationId = BarrelId;
        }

        public async Task<bool> CacheObject<T>(T model, int days) where T : _BaseModel
        {
            try
            {
                return await Task.Run(() =>
                { 
                    Barrel.Current.Add(model.Id.ToString(), model, TimeSpan.FromDays(days));

                    return true;
                });
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<T> GetObject<T>(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAllObjects<T>()
        {
            throw new NotImplementedException();
        }

        public async Task EmptyCache()
        {
            throw new NotImplementedException();
        }

   

       
    }
}
