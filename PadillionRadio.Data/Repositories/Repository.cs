using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PadillionRadio.Data.Contexts;
using PadillionRadio.Data.Interfaces;

namespace PadillionRadio.Data.Repositories
{
    public sealed class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly DatabaseContext context;
        private readonly DbSet<T> dbSet;

        public Repository(IUnitOfWork unitOfWork, DatabaseContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            
            query = includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> GetItem(long id)
        {
            var entity = dbSet.FindAsync(id);
            return await entity;
        }

        public Task<T[]> GetItems()
        {
           // context.Database.CloseConnection();
            var items = dbSet.ToArrayAsync();
            return items;
        }

        public void Update(T entity)
        {
            unitOfWork.Context.Entry(entity).State = EntityState.Modified; 
        }

        public void Detatch(T entity)
        {
            unitOfWork.Context.Entry(entity).State = EntityState.Detached;
        }
    }
}