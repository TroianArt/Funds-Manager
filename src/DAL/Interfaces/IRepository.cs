using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Interfaces
{
    /// <summary>
    /// Repository interface
    /// Contains methods Get, Delete, Update, Add async, Save, Save async
    /// </summary>
    /// <typeparam name="T"> entity typeparam</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// method of IRepository
        /// </summary>
        /// <returns>list of entities</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// method of IRepository
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>list of entities by predicate</returns>
        IEnumerable<T> Get(Func<T, bool> predicate);

        /// /// <summary>
        /// method of IRepository
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>entity by id</returns>
        T Get(int id);

        /// <summary>
        /// method of IRepository
        /// delete entity
        /// </summary>
        /// <param name="entity">entity</param>
        void Delete(T entity);

        /// <summary>
        /// method of IRepository
        /// delete entity by id
        /// </summary>
        /// <param name="id">id</param>
        void Delete(int id);

        /// <summary>
        /// method of IRepository
        /// update entity 
        /// </summary>
        /// <param name="entity">entity</param>
        void Update(T entity);

        /// <summary>
        /// method of IRepository
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>add task</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// method of IRepository
        /// Save changes
        /// </summary>
        void Save();

        /// <summary>
        /// method of IRepository
        /// </summary>
        /// <returns>saved changes</returns>
        Task SaveAsync();
    }
}