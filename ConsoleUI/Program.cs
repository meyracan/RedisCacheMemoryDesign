using Core.Concrete.RedisCache;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using ServiceStack.Redis;
using System.Text;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        EfUserDal efUserDal = new EfUserDal();
        RedisCacheManager redisCacheManager = new RedisCacheManager();

        //User user = new User();

        //user.Name = "ABC";
        //user.Surname = "CDE";
        

        /*efUserDal.GetAll();
        foreach (var item in efUserDal.GetAll())
        {
            Console.WriteLine(item.Id);
            Console.WriteLine(item.Name);
            Console.WriteLine(item.Surname);
        }*/

       
        User user1 = new User();
        user1.Id = 1;
        user1.Name = "meyra";
        user1.Surname = "can";

        User user2 = new User();
        user2.Id = 2;
        user2.Name = "jhfgdjf";
        user2.Surname = "mmm";

        redisCacheManager.HSet("users",user1.Id.ToString(), user1);
        redisCacheManager.HSet("users", user2.Id.ToString(), user2);
        //redisCacheManager.HSet("product", "name2", "computer");
        //string a = redisCacheManager.HGet<string>("product", "name");
        //string b = redisCacheManager.HGet<string>("product", "name2");
        //Console.WriteLine(a);
        //Console.WriteLine(b);
        //redisCacheManager.HDelete("product", "name");
        //redisCacheManager.HDeleteAll("product");
        var data = redisCacheManager.HGetAll<User>("users");
        Console.WriteLine(data[0].Name);


    }

}