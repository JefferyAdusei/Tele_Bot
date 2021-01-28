using System.Threading.Tasks;

namespace Bot.Core.Data.Interfaces
{
    /// <summary>
    /// Interface that holds write database execution methods
    /// </summary>
    public interface IWriteService
    {
        /// <summary>
        /// Method that saves a new client data to the backing database.
        /// </summary>
        /// <param name="dataModel">Data model representing the individual or group</param>
        /// <returns>A value indicating whether the action succeeded</returns>
        Task<bool> InsertRecordAsync<T>(T dataModel);

        /// <summary>
        /// Method that updates information in the backing database.
        /// </summary>
        /// <typeparam name="T">The provided type</typeparam>
        /// <param name="dataModel">Data model representing the individual or group</param>
        /// <returns>A value indicating whether the action succeeded</returns>
        Task<bool> UpdateDataAsync<T>(T dataModel);

        /// <summary>
        /// Deletes a record from the database.
        /// Actually uses soft delete instead
        /// </summary>
        /// <param name="id">The unique identifier of the record to remove</param>
        /// <returns></returns>
        Task<bool> DeleteRecordAsync(long id);
    }
}