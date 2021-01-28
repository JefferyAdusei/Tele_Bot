using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Core.ChatBot
{
    public static class BotExtensions
    {
        #region Message Reply Extensions
        /// <summary>
        /// Extension method that makes sending text replies easier.
        /// </summary>
        /// <param name="client">The bot client sending the text</param>
        /// <param name="chat">The chat the message is being sent to</param>
        /// <param name="message">The message to send</param>
        /// <param name="action">The chat action to show the user before replying</param>
        /// <returns></returns>
        public static async Task SendReplyAsync(this TelegramBotClient client, ChatId chat, string message, ChatAction action = ChatAction.Typing)
        {
            // Delay with reply with a chat action
            await client.SendActionAsync(chat);

            await client.SendTextMessageAsync(chat, message);
        }

        /// <summary>
        /// Extension method that sends reply as html
        /// </summary>
        /// <param name="client">The telegram bot client to use</param>
        /// <param name="chat">The chat to reply to</param>
        /// <param name="message">The text to send</param>
        /// <param name="action">The chat reply action</param>
        /// <returns></returns>
        public static async Task SendHtmlReplyAsync(this TelegramBotClient client, ChatId chat, string message, ChatAction action = ChatAction.Typing)
        {
            // Set chat action and delay
            await client.SendActionAsync(chat);

            // Send reply
            await client.SendTextMessageAsync(chat, message, ParseMode.Html, disableNotification: true);
        }

        /// <summary>
        /// Extension method that send replies with markups
        /// </summary>
        /// <param name="client">The telegram bot client.</param>
        /// <param name="chat">The chat to send reply to</param>
        /// <param name="message">The text to send</param>
        /// <param name="markup">The markup to use, keyboard or buttons</param>
        /// <returns></returns>
        public static async Task SendWithReplyMarkup(this TelegramBotClient client, Chat chat, string message,
            IReplyMarkup markup)
        {
            await client.SendActionAsync(chat);

            // Send reply with markup
            await client.SendTextMessageAsync(chat, message, ParseMode.Html, disableNotification: true,
                                              replyMarkup: markup);
        }

        #endregion

        #region Chat Action Extension

        /// <summary>
        /// Sends a chat action to the chat default being typing.
        /// </summary>
        /// <param name="client">The Telegram bot client</param>
        /// <param name="chat">The chat to send action to</param>
        /// <param name="action">The chat action to send</param>
        /// <returns></returns>
        public static async Task SendActionAsync(this TelegramBotClient client, ChatId chat,
            ChatAction action = ChatAction.Typing)
        {
            await client.SendChatActionAsync(chat, action);
            await Task.Delay(1000);
        }

        #endregion
    }
}