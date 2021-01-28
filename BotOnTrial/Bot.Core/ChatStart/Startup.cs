using System;
using Bot.Core.Di;
using Cored.Fabric.Di;
using Cored.Logging;

namespace Bot.Core.ChatStart
{
    public static class Startup
    {
        public static void Start()
        {
            CoreDi.BotServices.Client.OnUpdate += Client_OnUpdate;
            CoreDi.BotServices.Client.OnCallbackQuery += Client_OnCallbackQuery;
            CoreDi.BotServices.Client.OnReceiveError += Client_OnReceiveError;
            CoreDi.BotServices.Client.StartReceiving();
        }

        /// <summary>
        /// Process errors associated with receiving messages.
        /// </summary>
        /// <param name="sender">The object generating the error</param>
        /// <param name="e">The events associated with the error.</param>
        private static void Client_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            FabricDi.Logger.LogErrorSource($"{e.ApiRequestException.Message}");
        }

        /// <summary>
        /// Process callback queries sent by user.
        /// </summary>
        /// <param name="sender">The sender of the callback</param>
        /// <param name="e">Events associated with the callback</param>
        private static async void Client_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Console.WriteLine($"Processing Call back query {e.CallbackQuery.From.FirstName}");
            await CoreDi.MessageHandler.ProcessCallbackQueryAsync(e.CallbackQuery);
        }

        /// <summary>
        /// Process updates sent from the user.
        /// </summary>
        /// <param name="sender">The sender of the update</param>
        /// <param name="e">Event associated with the message</param>
        private static async void Client_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            if (e is null)
            {
                FabricDi.Logger.LogErrorSource("There was an error while receiving updates");
                return;
            }

            await CoreDi.UpdateServices.ProcessUpdateAsync(e.Update.Message);
        }
    }
}