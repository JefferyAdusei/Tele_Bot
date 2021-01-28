using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Core.Data;
using Bot.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data.DataService
{
    public class ReadDataService : IReadDataService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadDataService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to use for the query</param>
        public ReadDataService(BotDbContext dbContext)
        {
            // Set local member
            _botDbContext = dbContext;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly BotDbContext _botDbContext;

        #endregion

        #region Implementation of IReadDataService

        /// <inheritdoc />
        public async Task<BotDataModel> RetrieveUserAsync(string username) =>
            await _botDbContext.BotData.Where(botData => botData.Username == username)
                .FirstAsync();

        /// <inheritdoc />
        public async Task<BotDataModel> RetrieveUserAsync(int id, string passCode) =>
            await _botDbContext.BotData.Where(botData => botData.Id == id && botData.PassCode == passCode)
                .FirstAsync();

        /// <inheritdoc />
        public async Task<IAsyncEnumerable<BotDataModel>> RetrieveAllAsync() =>
            await Task.FromResult(_botDbContext.BotData.
                                      OrderBy(botData => botData.Username)
                                      .AsAsyncEnumerable());

        #endregion
    }
}