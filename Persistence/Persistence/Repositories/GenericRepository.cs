using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,new()
    {
        private readonly SimpleDbContext _simpleDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SimpleDbContext simpleDbContext)
        {
            _simpleDbContext = simpleDbContext;
            _dbSet= _simpleDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);  
           await _simpleDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(()=>_dbSet.Remove(entity));
            await _simpleDbContext.SaveChangesAsync();  
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(object id)
        {
           return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
           await Task.Run(()=> _dbSet.Update(entity));  
           await _simpleDbContext.SaveChangesAsync();
        }
    }
}
