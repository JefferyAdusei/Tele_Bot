using System.Threading.Tasks;
using Bot.Core.UserData;
using Telegram.Bot.Types;

namespace Bot.Core.Services.Interface
{
    /// <summary>
    /// Interface for managing groups
    /// </summary>
    public interface IGroupManager
    {
        /// <summary>
        /// Adds the new groups information to the group's database
        /// </summary>
        /// <param name="group">The group information to add to the group's data</param>
        /// <returns></returns>
        Task<bool> AddGroupAsync(Group group);

        /// <summary>
        /// Gets a groups information if the group already exist, or return null.
        /// </summary>
        /// <param name="chat">The chat information of the group</param>
        /// <returns></returns>
        Task<Group> GetGroupAsync(Chat chat);

        /// <summary>
        /// Updates group info if changed
        /// </summary>
        /// <param name="group">The group chat</param>
        /// <returns></returns>
        Task<bool> UpdateGroupInfoAsync(Group group);

        /// <summary>
        /// Sends private messages to group members privately
        /// </summary>
        /// <param name="chat">The group chat</param>
        /// <returns></returns>
        Task SendPrivateMessageAsync(Chat chat);

        /// <summary>
        /// Sends notifications to group members about any incoming events
        /// </summary>
        /// <param name="chat">The group chat</param>
        /// <returns></returns>
        Task NotifyGroupMembers(Chat chat);

        /// <summary>
        /// Adds a registered user to the group
        /// </summary>
        /// <param name="chat">The group chat</param>
        /// <returns></returns>
        Task AddUserToGroup(Chat chat);

        /// <summary>
        /// Removes a user from the group.
        /// </summary>
        /// <param name="chat">The group chat</param>
        /// <returns></returns>
        Task RemoveUserFromGroupAsync(Chat chat);
    }
}