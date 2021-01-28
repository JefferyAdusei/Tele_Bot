namespace Bot.Core
{
    /// <summary>
    /// Constants for managing the current state of the application.
    /// </summary>
    public class AppState
    {
        /// <summary>
        /// Indicates that app is in the main menu
        /// </summary>
        public const string MainMenu = "MainMenu";

        /// <summary>
        /// Application state management for signing up.
        /// </summary>
        public static class SignUp
        {
            /// <summary>
            /// Indicates app is in the fullname registration mode.
            /// </summary>
            public const string DateOfBirth = "DateOfBirth";

            /// <summary>
            /// Indicates app is in phone number registration mode.
            /// </summary>
            public const string Phone = "Phone";

            /// <summary>
            /// Indicates app has reached sign up completion mode.
            /// </summary>
            public const string Complete = "SignUpComplete";

            /// <summary>
            /// Indicates app is the view user profile mode
            /// </summary>
            public const string ViewProfile = "ViewProfile";

            /// <summary>
            /// Indicates app is in the mode for updating user profile.
            /// </summary>
            public const string UpdateProfile = "UpdateProfile";
        }
    }
}