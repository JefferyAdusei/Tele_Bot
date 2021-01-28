using Bot.Core.Di;
using Bot.Core.Services.Interface;
using Cored.Fabric.Di;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Bot.Core.ChatBot;
using Bot.Core.UserData;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Core.Services
{
    public class UpdateService : IUpdateService
    {
        #region Implementation of IUpdateService

        /// <inheritdoc />
        public async Task ProcessUpdateAsync(Message update)
        {
            // Log update received
            FabricDi.Logger.LogInformation("Update started successfully");

            if (update is null)
            {
                return;
            }

            // Return if we don't have a message.
            if (update.Type == MessageType.Unknown)
            {
                return;
            }

            if (!(update.LeftChatMember is null))
            {
                User member = update.LeftChatMember;
                Chat chat = update.Chat;

                if (member.IsBot)
                {
                    return;
                }
                // Remove the chat member from the group
                await CoreDi.ManageGroup.RemoveUserFromGroupAsync(chat);
            }

            // Switch on message type
            switch (update.Type)
            {
                // When the message type is a text
                case MessageType.Text:
                {
                    if (update.Chat.Type == ChatType.Private)
                        await CoreDi.MessageHandler.ProcessIndividualMessageAsync(update);
                    else
                        await CoreDi.MessageHandler.ProcessGroupMessageAsync(update);
                    break;
                }

                // When a new user has been added
                case MessageType.ChatMembersAdded:
                {
                    User[] members = update.NewChatMembers;

                    foreach (User member in members)
                    {
                        if (member.IsBot)
                        {
                            return;
                        }

                        // Check if the new user already exist in the database
                        Person person = await CoreDi.ManageUser.GetUserAsync(member);

                        if (person is null)
                        {
                            // Welcome new member and ask them to send a private message.
                            await CoreDi.BotServices.Client.SendHtmlReplyAsync(update.Chat,
                                                                               $"<strong>Hi {member.FirstName}</strong>,\n" +
                                                                               $"I am {update.Chat.Title}'s event manager\n" +
                                                                               $"<a href=\"tg://user?id={CoreDi.BotServices.Client.BotId}\"><em>Click Here</em></a> to get started...");
                            return;
                        }

                        person.GroupId = member.Id;

                        if (await CoreDi.ManageUser.UpdateUserAsync(person))
                        {
                            await CoreDi.BotServices.Client.SendReplyAsync(person.PersonId,
                                                                    $"Hi {person.NickName}, You are now a member of {update.Chat.Title}. Enjoy your stay");
                            return;
                        }
                    }
                    break;
                }

                // When a user has left the group chat
                case MessageType.ChatMemberLeft:
                {
                    User member = update.LeftChatMember;
                    Chat chat = update.Chat;

                    if (member is { } && member.IsBot)
                    {
                        return;
                    }
                    // Remove the chat member from the group
                    await CoreDi.ManageGroup.RemoveUserFromGroupAsync(chat);
                    break;
                }

                case MessageType.GroupCreated:
                {
                    #region Initialize

                    Chat chat = update.Chat;

                    // Check if group exists
                    Group group = await CoreDi.ManageGroup.GetGroupAsync(chat);

                    #endregion

                    if (group is null)
                    {
                        await CoreDi.ManageGroup.AddGroupAsync(new Group
                        {
                            GroupId = chat.Id,
                            GroupName = chat.Title,
                            Description = chat.Description,
                            InviteLink = chat.InviteLink
                        });
                    }
                    // TODO: Get number of chat members (getChatMembersCount(int chatId))
                    // TODO: Get all chat members and check if they exist in the user database.
                    break;
                }
                case MessageType.ChatTitleChanged:
                {
                    Chat chat = update.Chat;

                    Group group = await CoreDi.ManageGroup.GetGroupAsync(chat);

                    if (group is null)
                    {
                        return;
                    }

                    group.GroupName = chat.Title;

                    await CoreDi.ManageGroup.UpdateGroupInfoAsync(group);
                    break;
                }
            }
        }

        #endregion
    }
}