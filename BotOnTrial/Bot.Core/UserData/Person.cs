using System;

namespace Bot.Core.UserData
{
    /// <summary>
    /// Defines data that represents an individual
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the Id for the database
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Gets or sets the identifier for a person who has joined a group.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the fullname of the user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the nickname if any of the user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the phone number or the user
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Date of birth of the user
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the last seen date of the user
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// Gets or sets the registration state of the user.
        /// </summary>
        public string RegistrationState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has successfully signed up.
        /// </summary>
        public bool SignUpComplete { get; set; }

        /// <summary>
        /// Gets or sets the group id as foreign key referencing the group the user belongs to.
        /// </summary>
        public long GroupId { get; set; }
    }
}