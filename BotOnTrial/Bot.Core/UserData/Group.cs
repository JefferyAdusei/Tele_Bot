namespace Bot.Core.UserData
{
    public class Group
    {
        /// <summary>
        /// Gets or sets the groupId
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the groups description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets an invitation link to the group
        /// </summary>
        public string InviteLink { get; set; }
    }
}