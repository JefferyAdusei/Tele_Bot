namespace Bot.Core.Data.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that handles database commands
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Task that ensures that the database is correctly setup.
        /// </summary>
        /// <returns></returns>
        Task EnsureDataCreatedAsync();

        /// <summary>
        /// Task that clears all data from the database.
        /// </summary>
        /// <returns></returns>
        Task ClearAllDataAsync();
    }
}