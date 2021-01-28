using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Core.Data.Interfaces
{
    /// <summary>
    /// Interface for querying database for data about an individual
    /// </summary>
    public interface IReadService<T>
    {
        /// <summary>
        /// Retrieves a user from the database given the user's id
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="id">the unique identifier to lookup</param>
        /// <returns></returns>
        Task<T> RetrieveUserAsync(long id);

        /// <summary>
        /// An asynchronous method that searches for a user record based on the user id.
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="username">The username to enable scoping of search</param>
        /// <returns></returns>
        Task<T> RetrieveUserAsync(int id, string username);


        /// <summary>
        /// An asynchronous method returns all records from the database.
        /// </summary>
        /// <returns>A list of data/></returns>
        Task<IAsyncEnumerable<T>> RetrieveAllAsync();
    }
}