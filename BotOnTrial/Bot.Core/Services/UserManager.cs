using System.Threading.Tasks;
using Bot.Core.ChatBot;
using Bot.Core.Di;
using Bot.Core.Services.Interface;
using Bot.Core.UserData;
using Telegram.Bot.Types;

namespace Bot.Core.Services
{
    public class UserManager : IUserManager
    {
        #region Implementation of IUserManager

        /// <inheritdoc />
        public async Task GreetUser(Message message)
        {
            Chat chat = message.Chat;
            string text = message.Text.ToLower();
            if (text == "hi" || text == "hello" || text.Contains("start"))
            {
                await CoreDi.BotServices.Client.SendReplyAsync(chat,
                                                               $"Hi {chat.FirstName}, I am a (ro)bot assistant for your class group." +
                                                               "Please take these few steps to get us acquainted");

                await CoreDi.BotServices.Client.SendHtmlReplyAsync(chat, "<b>First type your name(Nickname)</b>\n" +
                                                                         "<i>Tip: Use a name your friends easily identifies you with</i>");
            }
        }

        /// <inheritdoc />
        public async Task<bool> AddNewUserAsync(Person person) =>
            await CoreDi.WriteService.InsertRecordAsync(person);

        /// <inheritdoc />
        public async Task<bool> UpdateUserAsync(Person person) =>
            await CoreDi.WriteService.UpdateDataAsync(person);

        /// <inheritdoc />
        public async Task<Person> GetUserAsync(Chat chat) =>
            await CoreDi.ReadPersonService.RetrieveUserAsync(chat.Id);

        /// <inheritdoc />
        public async Task<Person> GetUserAsync(User user) =>
            await CoreDi.ReadPersonService.RetrieveUserAsync(user.Id, user.Username);

        #endregion
    }
}