using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Core.ChatBot.DataExtensions;
using Bot.Core.Data;
using Bot.Core.Di;
using Cored.Fabric;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Core.ChatBot
{
    public static class Setup
    {
        #region Private Properties

        /// <summary>
        /// Configuration to contact the telegram bot api.
        /// </summary>
        private static readonly TelegramBotClient WhitsModelBot = new TelegramBotClient(Fabric.Construction.Configuration["BotToken:ApiToken"]);

        #endregion

        #region Bot Methods

        #region Startup

        /// <summary>
        /// Initialize the bot
        /// </summary>
        public static void StartBot()
        {
            // Listen for new messages
            WhitsModelBot.OnMessage += WhitsModelBot_OnMessage;


            WhitsModelBot.OnUpdate += WhitsModelBot_OnUpdate;

            // Start receiving
            WhitsModelBot.StartReceiving();

            Console.WriteLine("App Started.....");
        }

        #endregion


        #region Update Received

        private static void WhitsModelBot_OnUpdate(object sender, UpdateEventArgs e)
        {
            
        }

        #endregion

        #region On Message Received

        /// <summary>
        /// Handles On Message received events from the users of the chat bot.
        /// </summary>
        /// <param name="sender">The message sender</param>
        /// <param name="e">Events associated with the message</param>
        private static async void WhitsModelBot_OnMessage(object sender, MessageEventArgs e)
        {
            Chat chat = e.Message.Chat;

            if (e.Message.Type != MessageType.Text)
            {
                await WhitsModelBot.SendReplyAsync(chat, $"Hi {chat.FirstName},\n" +
                                                    $"I am still learning about {e.Message.Type.ToString().ToLower()}s.\n" +
                                                    "Why don't you text me instead!");
                return;
            }


            Console.WriteLine($"Processing '{e.Message.Text}' from {e.Message.Chat.Username}...");

            try
            {
                await ProcessCommandAsync(e);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Source}: {exception.Message}");
            }
        }

        #endregion

        private static async Task ProcessCommandAsync(MessageEventArgs e)
        {
            Chat chat = e.Message.Chat;
            string message = e.Message.Text;
            bool authenticate = await chat.Username.AuthenticateSenderAsync();

            // Greet user on new message arrivals with 'start' 'hello' or 'hi'
            await SalutationsAsync(chat, message);

            if (authenticate)
            {
                if (!message.Contains("="))
                {
                    await AuthorizeAsync(chat);
                }

                await UpdateUserDetailsAsync(chat, message);

                await PresentUserWithCommands(chat);

                await QueryForDataAsync(chat, message);
            }

            // Authenticate user
            await Authentication(e.Message, authenticate);

            //await AllowUserUpdatesAsync(chat);
        }

        #region Authentication

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="authenticate">indicates whether the user is authenticated</param>
        /// <returns></returns>
        private static async Task Authentication(Message message, bool authenticate)
        {
            var chat = message.Chat;

            if (!authenticate)
            {
                if (message.Text.ToLower().StartsWith("smu"))
                {
                    authenticate = await chat.Username.AddNewUserAsync(chat.FirstName, message.Text);

                    if (authenticate && !message.Text.Contains('=') && !message.Text.Contains('/'))
                    {
                        await AuthorizeAsync(chat);
                    }

                    return;
                }

                await WhitsModelBot.SendReplyAsync(chat,
                                                   "OOps! It seems you are not signed up yet.\n Kindly follow these steps below to register");
                await WhitsModelBot.SendReplyAsync(chat, "First type smu followed by your Pass code....\n" +
                                                         "Here is my example\n\n" +
                                                         "smu Pa$$C0d3");
            }
        }

        #endregion

        #region Authorization

        /// <summary>
        /// Presents user with an authorization message
        /// </summary>
        /// <param name="chat">The chat to send and receive messages from</param>
        /// <returns></returns>
        private static async Task AuthorizeAsync(Chat chat)
        {
            await WhitsModelBot.SendReplyAsync(chat,
                                               $"Welcome {chat.FirstName}!!!.\n Kindly follow this *STERP* to add or update your details");

            await WhitsModelBot.SendReplyAsync(chat, "*S*=_your skype id_\n" +
                                                     "*T*=_your team account name_\n" +
                                                     "*E*=_your official email_\n" +
                                                     "*R*=_your role_\n" +
                                                     "*P*=_your phone number\n\n" +
                                                     "Here is an example\n" +
                                                     "R=Office Assistant\n\n" +
                                                     "_This will add or my role as an office assistant to my details");
        }

        #endregion

        #region Update User Details

        /// <summary>
        /// Update user's details in the database.
        /// </summary>
        /// <param name="chat">the chat from which the update was received</param>
        /// <param name="message">The update syntax</param>
        /// <returns></returns>
        private static async Task UpdateUserDetailsAsync(Chat chat, string message)
        {
            if (!message.Contains("="))
            {
                return;
            }
            if (await chat.Username.UpdateUserInfo(message))
            {
                await WhitsModelBot.SendReplyAsync(chat, "Update was successful");
            }
            else
            {
                await WhitsModelBot.SendReplyAsync(chat, "An Error occurred while saving your data\n" +
                                                         "Please try again..");
            }
        }

        #endregion

        #region Present Commands

        /// <summary>
        /// Presents user with a list of available usernames
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        private static async Task PresentUserWithCommands(Chat chat)
        {
            string list = "";

            var command = await DataExtensions.DataExtensions.GetOfficeColleagues();

            var commands = new List<BotCommand>();

            if (command != null)
            {
                await foreach (var colleague in command)
                {
                    if (colleague.Username == chat.Username)
                    {
                        colleague.Firstname = "You";
                    }

                    list += $"\n{colleague.Id} - {colleague.Firstname}\n";
                    commands.Add(new BotCommand {Command = colleague.Id.ToString(), Description = colleague.Firstname});
                }
                await WhitsModelBot.SetMyCommandsAsync(commands);
            }

            await WhitsModelBot.SendReplyAsync(chat, $"Search For someone using '/' Followed by the user's Id " +
                                                     $"{list}");
        }

        #endregion

        #region Query For Data

        /// <summary>
        /// Queries for a colleagues account info.
        /// </summary>
        /// <param name="chat">The chat to reply to</param>
        /// <param name="message">The message to send</param>
        /// <returns></returns>
        private static async Task QueryForDataAsync(Chat chat, string message)
        {
            if (!message.StartsWith("/"))
            {
                return;
            }

            message = message.Split('/')[1];

            BotDataModel colleague = await chat.Username.GetColleagueAsync(message);

            if (colleague != null)
            {
                await WhitsModelBot.SendReplyAsync(chat, $"Contact Details For {colleague.Firstname}\n" +
                                                         $"Role : {colleague.Role}\n" +
                                                         $"Skype Id: {colleague.Skype}\n" +
                                                         $"Team Account Name: {colleague.TeamAccount}\n" +
                                                         $"Office Mail: {colleague.MailAccount}");
                await WhitsModelBot.SendContactAsync(chat, $"{colleague.Phone}", $"{colleague.Firstname}");
            }
        }

        #endregion

        #region Salutation

        /// <summary>
        /// Greets users on message received
        /// </summary>
        /// <param name="chat">The chat the message is coming from</param>
        /// <param name="message">The message sent</param>
        /// <returns></returns>
        private static async Task SalutationsAsync(Chat chat, string message)
        {
            // Convert message to lower case characters
            message = message.ToLower();

            if (message.StartsWith("hello") || message.StartsWith("hi") || message.StartsWith("start"))
            {
                await WhitsModelBot.SendReplyAsync(chat, $"Hi {chat.FirstName}, \n" +
                                                         "I'm your new Office Assistant....");

                await WhitsModelBot.SendReplyAsync(chat,
                                                   "Currently, I'm only limited to keeping records of your basic office information");
            }
        }

        #endregion

        #endregion
    }
}