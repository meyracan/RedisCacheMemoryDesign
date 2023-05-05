using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal : IUserDal
    {
        
        public void Add(User user)
        {
            using (RedisTestDbContext context = new RedisTestDbContext())
            { 
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Success");
            }
            
        }

        public void Delete(User user)
        {
            using (RedisTestDbContext context = new RedisTestDbContext())
            {
                context.Users.Remove(user);
                context.SaveChanges();
                Console.WriteLine("Success");
            }                
        }

        public List<User> GetAll()
        {
            using (RedisTestDbContext context = new RedisTestDbContext())
            {
               return context.Users.ToList();
            }
        }

        public User GetById(int userId)
        {
            using (RedisTestDbContext context = new RedisTestDbContext())
            {
                return context.Users.SingleOrDefault<User>(item => item.Id == userId); 
            }
        }

        public void Update(User user)
        {
            using (RedisTestDbContext context = new RedisTestDbContext())
            {
                context.Users.Update(user);
            }
        }
    }
}
