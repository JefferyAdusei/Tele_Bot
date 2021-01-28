using System.Threading.Tasks;
using Bot.Core.UserData;
using Telegram.Bot.Types;

namespace Bot.Core.Services.Interface
{
    public interface IUserManager
    {
        /// <summary>
        /// Send greetings to user
        /// </summary>
        /// <param name="message">The chat to send the message to</param>
        /// <returns></returns>
        Task GreetUser(Message message);

        /// <summary>
        /// Adds a new person to the person database
        /// </summary>
        /// <param name="person">The person model to add</param>
        /// <returns></returns>
        Task<bool> AddNewUserAsync(Person person);

        /// <summary>
        /// Updates user information in the database.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<bool> UpdateUserAsync(Person person);

        /// <summary>
        /// Retrieves a user from the database
        /// </summary>
        /// <param name="chat">The details of the person to retrieve</param>
        /// <returns></returns>
        Task<Person> GetUserAsync(Chat chat);

        /// <summary>
        /// Retrieves a user from the database for a group.
        /// </summary>
        /// <param name="user">The details of the user who joined the group</param>
        /// <returns></returns>
        Task<Person> GetUserAsync(User user);
    }
}