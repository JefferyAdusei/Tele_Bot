using System.Threading.Tasks;
using Bot.Core.Data.Interfaces;
using Bot.Core.UserData;

namespace Bot.Data.DataService
{
    public class WriteService : IWriteService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteService"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public WriteService(ClassContext dbContext)
        {
            // Set up local member.
            _classContext = dbContext;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// The database context for the client data store.
        /// </summary>
        private readonly ClassContext _classContext;

        #endregion

        #region Implementation of IWriteService

        /// <inheritdoc />
        public async Task<bool> InsertRecordAsync<T>(T dataModel)
        {
            if (dataModel is Group group)
            {
                // Add new record to the database
                await _classContext.Groups.AddAsync(group);

                // Save changes
                return await _classContext.SaveChangesAsync() > 0;
            }

            // Add new person to the database
            Person person = dataModel as Person;

            // Add new person record to the database
            await _classContext.Persons.AddAsync(person!);
            return await _classContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDataAsync<T>(T dataModel)
        {
            if (dataModel is Group group)
            {
                // Add new record to the database
                _classContext.Groups.Update(group);

                // Save changes
                return await _classContext.SaveChangesAsync() > 0;
            }

            // Add new person to the database
            Person person = dataModel as Person;

            // Add new person record to the database
            _classContext.Persons.Update(person!);
            return await _classContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRecordAsync(long id)
        {
            // TODO: Implement deleting using soft delete
            await Task.Delay(10);
            return true;
        }

        #endregion
    }
}