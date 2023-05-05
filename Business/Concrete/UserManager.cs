using Business.Abstract;
using Core.Abstract;
using Core.Concrete.RedisCache;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IRedisCacheService _redisCacheService;
        private IUserDal _userDal;

        public UserManager(IRedisCacheService  redisCacheService, IUserDal userDal)
        {
            _redisCacheService = redisCacheService;
            _userDal = userDal;
        }
        string hashId = "users";
        public void Add(User user)
        {
            if (_redisCacheService.HGet<User>(hashId,user.Id.ToString()) == null && _userDal.GetById(user.Id) == null)
            {                
                _userDal.Add(user);
                _redisCacheService.HSet(hashId, user.Id.ToString(), user);
            }
            else if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) != null && _userDal.GetById(user.Id) == null)
            {
                _userDal.Add(user);
            }
            else if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) == null && _userDal.GetById(user.Id) != null)
            {
                _redisCacheService.HSet(hashId, user.Id.ToString(), user);
            }
            else
            {
               // Console.WriteLine("Data is already exist!");
            }           
        }
        public void Delete(User user)
        {
            if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) == null && _userDal.GetById(user.Id) == null)
            {
                Console.WriteLine("Data is not exist!");
            }
            else if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) != null && _userDal.GetById(user.Id) == null)
            {
                _redisCacheService.HDelete(hashId, user.Id.ToString());
            }
            else if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) == null && _userDal.GetById(user.Id) != null)
            {
                _userDal.Delete(user);
            }
            else if (_redisCacheService.HGet<User>(hashId, user.Id.ToString()) != null && _userDal.GetById(user.Id) != null)
            {
                _redisCacheService.HDelete(hashId, user.Id.ToString());
                _userDal.Delete(user);
            }
        }
        public List<User> GetAll()
        {
            if (_redisCacheService.HGetAll<User>(hashId) != null)
            {
                List<User> list = new List<User>();
                list = _redisCacheService.HGetAll<User>(hashId);
                return list;
            }
            else if (_redisCacheService.HGetAll<User>(hashId) == null && _userDal.GetAll() != null)
            {
                List<User> list = new List<User>();
                list = _userDal.GetAll();
                foreach (var item in list)
                {
                    _redisCacheService.HSet(hashId, item.Id.ToString(), JsonConvert.SerializeObject(item));
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public User GetById(int Id)
        {            
            if (_redisCacheService.HGet<User>(hashId, Id.ToString()) != null)
            {
                return _redisCacheService.HGet<User>(hashId,Id.ToString());
            }
            else if (_redisCacheService.HGet<User>(hashId, Id.ToString()) == null && _userDal.GetById(Id) != null)
            {
                User user = _userDal.GetById(Id);
                _redisCacheService.HSet(hashId, Id.ToString(), user);
                return user;
            }
            else 
            {
                return null; 
            }          
        }
        public void Update(User user)
        {
            if (_userDal.GetById(user.Id) == null)
            {
                Console.WriteLine("User is not exist!");
            }
            else if (_userDal.GetById(user.Id) != null && _redisCacheService.HGet<User>(hashId, user.Id.ToString()) != null
                || _userDal.GetById(user.Id) != null && _redisCacheService.HGet<User>(hashId, user.Id.ToString()) == null)
            {
                _userDal.Update(user);
                _redisCacheService.HSet(hashId,user.Id.ToString(), user);
            }
        }
    }
}
