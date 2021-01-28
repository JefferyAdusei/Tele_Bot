using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Core.Data.Interfaces;
using Bot.Core.UserData;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data.DataService
{
    public class PersonReadService : IReadService<Person>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonReadService"/> class.
        /// </summary>
        /// <param name="classContext"></param>
        public PersonReadService(ClassContext classContext)
        {
            // Set local member
            _classContext = classContext;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly ClassContext _classContext;

        #endregion

        #region Implementation of IReadService

        /// <inheritdoc />
        public async Task<Person> RetrieveUserAsync(long id)
        {
            return await _classContext.Persons.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<Person> RetrieveUserAsync(int id, string username)
        {
            var result = await _classContext.Persons.Where(p => p.UserName == username)
                .FirstOrDefaultAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<IAsyncEnumerable<Person>> RetrieveAllAsync()
        {
            return await Task.FromResult(_classContext.Persons.OrderBy(p => p.UserName).AsAsyncEnumerable());
        }

        #endregion
    }
}