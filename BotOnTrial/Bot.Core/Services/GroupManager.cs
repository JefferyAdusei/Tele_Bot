using System.Threading.Tasks;
using Bot.Core.Di;
using Bot.Core.Services.Interface;
using Bot.Core.UserData;
using Telegram.Bot.Types;

namespace Bot.Core.Services
{
    public class GroupManager : IGroupManager
    {
        #region Implementation of IGroupManager

        /// <inheritdoc />
        public async Task<bool> AddGroupAsync(Group group) =>
            await CoreDi.WriteService.InsertRecordAsync(group);

        /// <inheritdoc />
        public async Task<Group> GetGroupAsync(Chat chat) =>
            await CoreDi.ReadGroupService.RetrieveUserAsync(chat.Id);

        /// <inheritdoc />
        public async Task<bool> UpdateGroupInfoAsync(Group group)
        {
            return await CoreDi.WriteService.UpdateDataAsync(group);
        }

        /// <inheritdoc />
        public async Task SendPrivateMessageAsync(Chat chat)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task NotifyGroupMembers(Chat chat)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task AddUserToGroup(Chat chat)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async Task RemoveUserFromGroupAsync(Chat chat)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}