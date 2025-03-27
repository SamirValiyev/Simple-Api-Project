using Application.Interfaces;
using Domain;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SimpleDbContext simpleDbContext) : base(simpleDbContext)
        {
        }
    }
}
