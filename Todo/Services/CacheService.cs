using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Todo.Infrastructure.Exceptions;
using Todo.Models;
using System.Linq;

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

        public async Task<bool> CacheObject<T>(string serviceName, T model, int days) where T : _BaseModel
        {
            try
            {
                return await Task.Run(() =>
                { 
                    Barrel.Current.Add(key: serviceName, model, TimeSpan.FromDays(days));

                    return true;
                });
            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return false;
            }
        }

        public async Task<T> GetObject<T>(string serviceName, Guid modelId) where T : _BaseModel
        {
            try
            {
                return await Task.Run(() =>
                {
                    var results = Barrel.Current.Get<List<T>>(key: serviceName);
                    var result = results.Where(x => x.Id == modelId).FirstOrDefault();

                    return result;
                });

            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return null;
            }
        }

        public async Task<List<T>> GetAllObjects<T>(string serviceName)
        {
            List<T> collection = new List<T>();

            try
            {
                return await Task.Run(() =>
                {
                    return Barrel.Current.Get<List<T>>(key: serviceName);
                });

            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
                return null;
            }
        }

        public async Task EmptyCache()
        {
            try
            {
                await Task.Run(() =>
                {
                    Barrel.Current.EmptyAll();
                });

            }
            catch (Exception ex)
            {
                ErrorTracker.ReportError(ex);
            }
        }




    }
}
