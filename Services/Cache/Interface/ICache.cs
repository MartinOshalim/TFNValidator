using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabGroup_Task.Services.Cache.Interface
{
    public interface ICache
    {
        public bool AddCacheEntry(object key);
        public object GetCacheEntry(object key);
    }
}
