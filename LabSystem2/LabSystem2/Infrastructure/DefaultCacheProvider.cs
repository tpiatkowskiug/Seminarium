using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace LabSystem2.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache Cache { get { return HttpContext.Current.Cache; } }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Set(string key, object data, int cacheTimeMinutes)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(cacheTimeMinutes);

            Cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }
    }
}
