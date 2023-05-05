using Core.Abstract;
using Entities.Concrete;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

namespace Core.Concrete.RedisCache
{
    public class RedisCacheManager : IRedisCacheService
    {
        public void HDelete(string hashId, string key)
        {
            using (IRedisClient client = new RedisClient())
            {
                var result = client.RemoveEntryFromHash(hashId, key);
            }
        }
        public void HDeleteAll(string hashId)
        {
            using (IRedisClient client = new RedisClient())
            {
                var result = client.Remove(hashId);
            }
        }
        public T? HGet<T>(string hashId,string key)
        {
            using (IRedisClient client = new RedisClient())
            {
                var result = client.GetValueFromHash(hashId, key);
                if (result==null)
                {
                    return default(T);
                }
                else
                {
                    T t = JsonConvert.DeserializeObject<T>(result);
                    return t;
                }                
            }
        }
       public List<T> HGetAll<T>(string hashId)
        {
            using (IRedisClient client = new RedisClient())
            {
                List<T> list = new List<T>();                
                var result = client.GetHashValues(hashId);
                foreach (var item in result)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item));              
                }                
                return list;
            }
        }
        public void HSet(string hashId, string key, Object value)
        {
            using (IRedisClient client = new RedisClient())
            {
                string jsonString = JsonConvert.SerializeObject(value);
                client.SetEntryInHash(hashId, key, jsonString);               
            }
        }
    }
}
