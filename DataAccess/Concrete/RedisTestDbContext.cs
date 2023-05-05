using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    internal class RedisTestDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-ASUN2DV\SQLEXPRESS;
                                        Database=RedisDeneme;
                                        Trusted_Connection=true");
        }
        public DbSet<User> Users { get; set; }
    }
}

