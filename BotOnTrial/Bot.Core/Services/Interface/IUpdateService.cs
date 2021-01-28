using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Core.Services.Interface
{
    public interface IUpdateService
    {
        /// <summary>
        /// Task that handles receipt of a telegram update.
        /// </summary>
        /// <param name="update">The update received from the sender</param>
        /// <returns></returns>
        Task ProcessUpdateAsync(Message update);
    }
}