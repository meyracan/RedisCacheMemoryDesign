﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        void Add(User user);
        void Delete(User user);
        List<User> GetAll();
        User GetById(int userId);
        void Update(User user);
    }
}
