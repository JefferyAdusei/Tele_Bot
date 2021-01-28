using Telegram.Bot;

namespace Bot.Core.Services.Interface
{
    /// <summary>
    /// Interface for managing the bot client.
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        /// Gets the telegram bot client to communicate through.
        /// </summary>
        TelegramBotClient Client { get; }
    }
}