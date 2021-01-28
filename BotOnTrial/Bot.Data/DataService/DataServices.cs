using System.Threading.Tasks;
using Bot.Core.Data.Interfaces;

namespace Bot.Data.DataService
{
    /// <summary>
    /// Implementation of <see cref="IDataService"/>
    /// </summary>
    public class DataServices : IDataService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DataServices"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public DataServices(ClassContext dbContext)
        {
            // Set local members
            _classContext = dbContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Database context for the client data store.
        /// </summary>
        private readonly ClassContext _classContext;

        #endregion

        #region Implementation of IDataService

        /// <inheritdoc />
        public async Task EnsureDataCreatedAsync()
        {
            await _classContext.Database.EnsureCreatedAsync();
        }

        /// <inheritdoc />
        public async Task ClearAllDataAsync()
        {
            await _classContext.Database.EnsureDeletedAsync();
        }

        #endregion
    }
}