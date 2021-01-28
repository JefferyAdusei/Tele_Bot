using System.Linq;
using System.Threading.Tasks;
using Bot.Core.Data;
using Bot.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data.DataService
{
    public class WriteDataService : IWriteDataService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteDataService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to use.</param>
        public WriteDataService(BotDbContext dbContext)
        {
            // Set local member
            _botDbContext = dbContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly BotDbContext _botDbContext;

        #endregion

        #region Implementation of IWriteDataService

        /// <inheritdoc />
        public async Task<bool> InsertRecordAsync(BotDataModel botDataModel)
        {
            // Add new record to the database
            await _botDbContext.BotData.AddAsync(botDataModel);

            // Save changes
            return await _botDbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDataAsync(string property, object updateValue, string username)
        {
            switch (property)
            {
                case "s":
                    _botDbContext.BotData
                        .Where(botData => botData.Username == username)
                        .FirstAsync().Result.Skype = (string) updateValue;
                    return await _botDbContext.SaveChangesAsync() > 0;

                case "t":
                    _botDbContext.BotData
                        .Where(botData => botData.Username == username)
                        .FirstAsync().Result.TeamAccount = (string) updateValue;
                    return await _botDbContext.SaveChangesAsync() > 0;

                case "e":
                    _botDbContext.BotData
                        .Where(botData => botData.Username == username)
                        .FirstAsync().Result.MailAccount = (string) updateValue;
                    return await _botDbContext.SaveChangesAsync() > 0;

                case "r":
                     _botDbContext.BotData
                         .Where(botData => botData.Username == username)
                         .FirstAsync().Result.Role = (string) updateValue;
                     return await _botDbContext.SaveChangesAsync() > 0;

                case "p":
                    _botDbContext.BotData
                        .Where(botData => botData.Username == username)
                        .FirstAsync().Result.Phone = (string) updateValue;
                    return await _botDbContext.SaveChangesAsync() > 0;

                default: return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRecordAsync(string username)
        {
            // Soft Delete record
            _botDbContext.BotData.FindAsync(username).Result.IsDeleted = true;

            return await _botDbContext.SaveChangesAsync() > 0;
        }

        #endregion
    }
}