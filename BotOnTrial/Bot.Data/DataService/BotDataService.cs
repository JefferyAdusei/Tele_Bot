using System.Threading.Tasks;
using Bot.Core.Data.Interfaces;

namespace Bot.Data.DataService
{
    public class BotDataService : IBotDataService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BotDataService"/> class.
        /// </summary>
        /// <param name="dbContext">The db context to use</param>
        public BotDataService(BotDbContext dbContext)
        {
            // Set local members
            _botDbContext = dbContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly BotDbContext _botDbContext;

        #endregion

        #region Implementation of IBotDataService

        /// <inheritdoc />
        public async Task EnsureDataCreatedAsync()
        {
            // Make sure the database exists and is created
            await _botDbContext.Database.EnsureCreatedAsync();
        }

        /// <inheritdoc />
        public async Task ClearAllDataAsync()
        {
            await _botDbContext.Database.EnsureDeletedAsync();
        }

        #endregion
    }
}