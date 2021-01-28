namespace Bot.Core.Constants
{
    /// <summary>
    /// Shows the current session mode of an ongoing session
    /// </summary>
    public static class SessionMode
    {
        /// <summary>
        /// Indicates interaction using the keyboard
        /// </summary>
        public const string Keyboard = "keyboard";

        /// <summary>
        /// Indicates interaction via the inline keyboard
        /// </summary>
        public const string InlineKeyboard = "InlineKeyboard";

        /// <summary>
        /// Indicates interaction via simple text
        /// </summary>
        public const string Text = "Text";
    }
}