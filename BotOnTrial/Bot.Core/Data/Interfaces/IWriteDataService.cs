using System.Threading.Tasks;

namespace Bot.Core.Data.Interfaces
{
    /// <summary>
    /// Interface that holds write database execution methods
    /// </summary>
    public interface IWriteDataService
    {
        /// <summary>
        /// Method that saves a new client data to the backing database.
        /// </summary>
        /// <param name="botDataModel">Data model representing the individual</param>
        /// <returns>A value indicating whether the action succeeded</returns>
        Task<bool> InsertRecordAsync(BotDataModel botDataModel);

        /// <summary>
        /// Method that updates a user's information in the database
        /// </summary>
        /// <param name="property">The property of the client information to update</param>
        /// <param name="updateValue">New value to be sent as update</param>
        /// <param name="username">Unique Id that identifies the user</param>
        /// <returns>A boolean indicating whether the action was successful</returns>
        Task<bool> UpdateDataAsync(string property, object updateValue, string username);

        /// <summary>
        /// Deletes a record from the database.
        /// Actually uses soft delete instead
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> DeleteRecordAsync(string username);
    }
}