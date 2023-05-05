using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IRedisCacheService
    {
        void HSet(string hashId, string key, Object value);
        T HGet<T>(string hashId, string key);
        List<T> HGetAll<T>(string hashId);
        void HDelete(string hashId, string key);
        void HDeleteAll(string hashId);

    }
}
