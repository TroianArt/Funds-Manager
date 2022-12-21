using System;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Interfaces
{
    /// <summary>
    /// Unit of Work interface
    /// Contains methods Save, Save async, Repository
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Implementation of IUnitOfWork
        /// </summary>
        /// <typeparam name="T">typeparam of repository</typeparam>
        /// <returns>repository by type</returns>
        public IRepository<T> Repository<T>() where T : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        public void Save();

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>saved changes</returns>
        public Task SaveAsync();
    }
}