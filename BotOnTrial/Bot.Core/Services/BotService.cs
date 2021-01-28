using Bot.Core.Services.Interface;
using Cored.Fabric;
using Telegram.Bot;

namespace Bot.Core.Services
{
    /// <summary>
    /// <inheritdoc cref="IBotService"/>
    /// </summary>
    public class BotService : IBotService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BotService"/> class.
        /// </summary>
        public BotService()
        {
            Client = new TelegramBotClient(Fabric.Construction.Configuration["BotToken:ApiToken"]);
        }

        #endregion

        #region Implementation of IBotService

        /// <inheritdoc />
        public TelegramBotClient Client { get; }

        #endregion
    }
}