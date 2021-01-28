namespace Bot.Core.Data
{
    /// <summary>
    /// Representation of the user details for the bot application
    /// </summary>
    public class BotDataModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique id of any individual
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the telegram user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the firstname of the telegram user.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the Pass code for authorizing users as part of a company
        /// </summary>
        public string PassCode { get; set; }

        /// <summary>
        /// Gets or sets the team account user name of an individual
        /// </summary>
        public string TeamAccount { get; set; }

        /// <summary>
        /// Gets or sets the skype id for an individual
        /// </summary>
        public string Skype { get; set; }

        /// <summary>
        /// Gets or sets the company mail address of an individual
        /// </summary>
        public string MailAccount { get; set; }

        /// <summary>
        /// Gets or sets  the Phone number of an individual
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the role of an individual.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the record has been deleted.
        /// The value is removed from the query.
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion
    }
}