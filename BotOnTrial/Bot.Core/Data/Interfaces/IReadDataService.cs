using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Core.Data.Interfaces
{
    /// <summary>
    /// Interface for querying database for data about an individual
    /// </summary>
    public interface IReadDataService
    {
        /// <summary>
        /// An asynchronous method that searches for a record from the database
        /// using a supplied username
        /// </summary>
        /// <param name="username">The supplied username to query for.</param>
        /// <returns>A single <see cref="BotDataModel"/></returns>
        Task<BotDataModel> RetrieveUserAsync(string username);

        /// <summary>
        /// An asynchronous method that searches for a user record based on the user id.
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="passCode">The passCode to enable scoping of search</param>
        /// <returns></returns>
        Task<BotDataModel> RetrieveUserAsync(int id, string passCode);


        /// <summary>
        /// An asynchronous method returns all records from the database.
        /// </summary>
        /// <returns>A list of <see cref="BotDataModel"/></returns>
        Task<IAsyncEnumerable<BotDataModel>> RetrieveAllAsync();
    }
}