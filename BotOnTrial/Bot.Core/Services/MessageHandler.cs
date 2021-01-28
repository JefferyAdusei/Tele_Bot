using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Core.ChatBot;
using Bot.Core.Constants;
using Bot.Core.Di;
using Bot.Core.Helpers;
using Bot.Core.UserData;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static Bot.Core.Di.CoreDi;


namespace Bot.Core.Services
{
    public class MessageHandler : Interface.IMessageHandler
    {
        #region Implementation of IMessageHandler

        /// <inheritdoc />
        public async Task ProcessIndividualMessageAsync(Message message)
        {
            #region Initialize

            // Get the representation of the individual.
            Chat chat = message.Chat;

            // Check if user exist in the database or return null.
            Person person = await ManageUser.GetUserAsync(chat);

            // If there is no registered user, register a new user.
            if (person is null)
            {
                // If Greet user and inform them about the bots functions
                await ManageUser.GreetUser(message);

                await ManageUser.AddNewUserAsync(new Person
                {
                    PersonId = chat.Id,
                    FullName = $"{chat.FirstName} {chat.LastName}",
                    SignUpComplete = false,
                    LastSeen = DateTime.Now.Date,
                    UserName = chat.Username,
                    RegistrationState = AppState.SignUp.UpdateProfile
                });

                // Exit until user gives information
                return;
            }

            #endregion

            // If there is a user.... continue to update their user information

            #region Update Nickname

            // Check if user is required to update their profile - Nickname
            if (person.RegistrationState == AppState.SignUp.UpdateProfile)
            {
                person.NickName = message.Text;
                if (await ManageUser.UpdateUserAsync(person))
                {
                    // Send reply to user to continue to add their phone number
                    await BotServices.Client.SendHtmlReplyAsync(chat, $"Hi {message.Text},\n <b>Enter your phone number</b>\n" +
                                                                      "<i>Tip: +2330501234567 or +(123) 0501234567</i>");

                    // Update session to phone
                    person.RegistrationState = AppState.SignUp.Phone;
                    await ManageUser.UpdateUserAsync(person);

                    // Exit
                    return;
                }

                // TODO: Create a function to send error messages
                await BotServices.Client.SendReplyAsync(chat,
                                                              "Something went wrong from my side, Pleas try again.");
                return;
            }

            #endregion

            #region Update Phone

            // Check if user is required to update their profile - Phone number
            if (person.RegistrationState == AppState.SignUp.Phone)
            {
                if (!message.Text.IsPhoneNumber())
                {
                    await BotServices.Client.SendReplyAsync(chat,
                                                                  $"{message.Text} - Is not a valid phone number.\n Please try again");
                    return;
                }
                person.PhoneNumber = message.Text;
                if (await ManageUser.UpdateUserAsync(person))
                {
                    // Send reply to user to continue to add their birthday
                    await BotServices.Client.SendHtmlReplyAsync(chat, "<b>Now add your birthday</b>\n" +
                                                                             "<i>Tip: Format (dd/mm/yyyy) (dd-mm-yyyy)</i>");

                    // Update session to phone
                    person.RegistrationState = AppState.SignUp.DateOfBirth;
                    await ManageUser.UpdateUserAsync(person);

                    // Exit
                    return;
                }
                // TODO: Send an error message
                return;
            }

            #endregion

            #region Update Date of Birth

            // Check if user is required to update their profile - Date of birth
            if (person.RegistrationState == AppState.SignUp.DateOfBirth)
            {
                if (!message.Text.IsDate())
                {
                    await CoreDi.BotServices.Client.SendHtmlReplyAsync(chat,
                                                                  $"<b>{message.Text}</b> - Is not a valid date (dd/mm/yyyy).\n Please try again");
                    return;
                }

                person.DateOfBirth = DateTime.Parse(message.Text);
                if (await ManageUser.UpdateUserAsync(person))
                {
                    List<KeyboardButton> keys = new List<KeyboardButton>
                    {
                        new KeyboardButton("What Next?")
                    };
                    // Send reply to user to continue to add their birthday
                    await CoreDi.BotServices.Client.SendWithReplyMarkup(chat,
                                                                        "<b>Congratulations!!</b> - Your profile update is complete",
                                                                        new ReplyKeyboardMarkup(keys, true, true));

                    // Update session and sign-up to complete
                    person.RegistrationState = AppState.SignUp.Complete;
                    person.SignUpComplete = true;
                    await ManageUser.UpdateUserAsync(person);

                    // Exit
                    return;
                }

                // TODO: Send an error message

                return;
            }

            #endregion

            #region Show Menu

            // User is successfully logged in show menu to either update existing details
            if (person.SignUpComplete && person.RegistrationState == AppState.SignUp.Complete)
            {
                List<InlineKeyboardButton> keys = new List<InlineKeyboardButton>()
                {
                    new InlineKeyboardButton{Text = "My Profile", CallbackData = Buttons.Profile},
                    //new InlineKeyboardButton{Text = "Main Menu", CallbackData = Buttons.JoinGroup},
                    //new InlineKeyboardButton{Text = "Go To Group", CallbackData = "GoToPage"}
                };

                InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(keys);

                await BotServices.Client.SendWithReplyMarkup(chat,
                                                                    $"Alright {chat.FirstName}, we are setup successfully\n" +
                                                                    $"You can take a look your information or go back to the main menu",
                                                                    inlineKeyboard);
            }

            #endregion
        }

        /// <inheritdoc />
        public async Task ProcessGroupMessageAsync(Message message)
        {
            #region Initialize

            Chat chat = message.Chat;

            // Get the sender of the message
            User sender = message.From;

            // Check if group exists
            Group group = await ManageGroup.GetGroupAsync(chat);

            #endregion

            if (group is null)
            {
                await ManageGroup.AddGroupAsync(new Group
                {
                    GroupId = chat.Id,
                    GroupName = chat.Title,
                    Description = chat.Description,
                    InviteLink = chat.InviteLink
                });

                return;
            }

            // Return if the sender is a bot
            if (sender.IsBot)
            {
                return;
            }

            // Get the sender's details
            var person = await ManageUser.GetUserAsync(sender);

            if (person is null)
            {
                await BotServices.Client.SendHtmlReplyAsync(chat,
                                                                   $"Hi {sender.FirstName}, you are <strong>required</strong> to send me a message " +
                                                                   "as a standard procedure for this group\n" +
                                                                   $"<a href=\"tg://user?id={BotServices.Client.BotId}\"><em>Click Here</em></a> to begin...");
                return;
            }

            // Check if the person has been active today
            if (person.LastSeen.Day != DateTime.Today.Day)
            {
                // If the person has not, update their last seen to the current day.
                person.LastSeen = DateTime.Today;
                person.GroupId = group.GroupId;
                await ManageUser.UpdateUserAsync(person);
            }
        }

        /// <inheritdoc/>
        public async Task ProcessCallbackQueryAsync(CallbackQuery query)
        {
            #region Initialize

            // Get the representation of the individual.
            Chat chat = query.Message.Chat;

            // Check if user exist in the database or return null.
            Person person = await ManageUser.GetUserAsync(chat);

            #endregion

            switch (query.Data)
            {
                case Buttons.Profile:
                {
                    await BotServices.Client.SendWithReplyMarkup(chat,
                                                                 $"<strong>Profile ({person.UserName})</strong>\n\n" +
                                                                 $"<strong>Name:</strong> {person.FullName}\n" +
                                                                 $"<strong>NickName:</strong> {person.NickName}\n" +
                                                                 $"<strong>Name:</strong> {person.DateOfBirth:D}\n" +
                                                                 $"<strong>Phone:</strong> {person.PhoneNumber}\n",
                                                                 new ReplyKeyboardMarkup(new List<KeyboardButton>
                                                                 {
                                                                     new KeyboardButton {Text = "Home"}
                                                                 }){ResizeKeyboard = true, OneTimeKeyboard = true});
                    break;
                }
                case Buttons.JoinGroup:
                {
                    await BotServices.Client.ExportChatInviteLinkAsync(chat);
                    await BotServices.Client.SendHtmlReplyAsync(chat, "Goto group");
                    break;
                }
            }
        }
        #endregion
    }
}