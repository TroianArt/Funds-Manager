using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository class
    /// Implement IRepository
    /// </summary>
    /// <typeparam name="T">entity typeparam</typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FundsContext context;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        public Repository()
        {
            this.context = new FundsContext();
            this.dbSet = this.context.Set<T>();
            this.dbSet.Load();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">context</param>
        public Repository(FundsContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        /// <summary>
        /// Implementation of IRepository
        /// </summary>
        /// <returns>list of entities</returns>
        public IEnumerable<T> Get()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Implementation of IRepository
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>entity by id</returns>
        public T Get(int id)
        {
            return this.dbSet.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Implementation of IRepository
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>list of entities by predicate</returns>
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return this.dbSet.ToList().Where(predicate);
        }

        /// <summary>
        /// Implementation of IRepository
        /// delete entity
        /// </summary>
        /// <param name="entity">entity</param>
        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        /// <summary>
        /// Implementation of IRepository
        /// delete entity by id
        /// </summary>
        /// <param name="id">id</param>
        public void Delete(int id)
        {
            T elem = this.dbSet.Find(id);
            if (elem != null)
            {
                this.dbSet.Remove(elem);
            }
        }

        /// <summary>
        /// Implementation of IRepository
        /// update entity 
        /// </summary>
        /// <param name="entity">entity</param>
        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }

        /// <summary>
        /// Implementation of IRepository
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>add task</returns>
        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Implementation of IRepository
        /// Save changes
        /// </summary>
        public void Save()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Implementation of IRepository
        /// </summary>
        /// <returns>saved changes</returns>
        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
