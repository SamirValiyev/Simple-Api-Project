﻿using Application.Interfaces;
using Domain;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        public UserRepository(SimpleDbContext simpleDbContext) : base(simpleDbContext)
        {
        }
    }
}
