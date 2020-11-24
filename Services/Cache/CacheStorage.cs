using LabGroup_Task.Enums;
using LabGroup_Task.Services.Cache.Interface;
using LabGroup_Task.Services.Logger.Interface;
using LabGroup_Task.Services.Options;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Cache
{
    public class CacheStorage : ICache
    {
        IMemoryCache _cache;
        ILogger _logger;
        RunOptions _runOptions;
        public CacheStorage(IMemoryCache cache, ILogger logger, RunOptions runOptions)
        {
            _cache = cache;
            _logger = logger;
            _runOptions = runOptions;
        }

        public bool AddCacheEntry(object key)
        {
            try
            {
                if (!_cache.TryGetValue(key, out var result))
                {
                    _logger.Log(LogType.Debug, $"Key doesn't exist, adding to cache key: {key}");
                    _cache.Set(key, key, TimeSpan.FromSeconds(_runOptions.CacheSeconds != 0 ? _runOptions.CacheSeconds : 30));
                }
            }
            catch(Exception e)
            {
                _logger.Log(LogType.Error, $"Error while adding entry to cache: {e.Message}");
                return false;
            }

            return true;
        }

        public object GetCacheEntry(object key)
        {
            if (_cache.TryGetValue(key, out var result))
            {
                return result;
            }

            return null;
        }


    }
}
