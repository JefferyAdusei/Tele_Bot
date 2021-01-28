using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Core.Data.Interfaces;
using Bot.Core.UserData;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data.DataService
{
    public class GroupReadService : IReadService<Group>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupReadService"/> class.
        /// </summary>
        /// <param name="classContext">The db context</param>
        public GroupReadService(ClassContext classContext)
        {
            _classContext = classContext;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly ClassContext _classContext;

        #endregion

        #region Implementation of IReadService<Group>

        /// <inheritdoc />
        public async Task<Group> RetrieveUserAsync(long id)
        {
            return await _classContext.Groups.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<Group> RetrieveUserAsync(int id, string username)
        {
            await Task.Delay(10);
            return null;
        }

        /// <inheritdoc />
        public async Task<IAsyncEnumerable<Group>> RetrieveAllAsync()
        {
            return await Task.FromResult(_classContext.Groups.OrderBy(g => g.GroupName).AsAsyncEnumerable());
        }

        #endregion
    }
}