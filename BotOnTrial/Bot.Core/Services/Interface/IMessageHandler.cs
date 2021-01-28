using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Core.Services.Interface
{
    /// <summary>
    /// Interface to handle all incoming messages
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Handles messages sent from individuals
        /// </summary>
        /// <param name="message">The message received</param>
        /// <returns></returns>
        Task ProcessIndividualMessageAsync(Message message);

        /// <summary>
        /// Handles messages received from groups
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ProcessGroupMessageAsync(Message message);

        /// <summary>
        /// Handles callback queries
        /// </summary>
        /// <param name="query">The call back query data</param>
        /// <returns></returns>
        Task ProcessCallbackQueryAsync(CallbackQuery query);
    }
}