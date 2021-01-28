namespace Bot.Core.Constants
{
    public static class Helper
    {
        /// <summary>
        /// Gets a constant string to match phone numbers
        /// </summary>
        public const string PhoneRegex = @"^[\+]?([(]?[0-9]{3}[)]?)?[0]?[0-5][0-9][-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        /// <summary>
        /// Gets the constant regex string to match date
        /// </summary>
        public const string DateRegex =
            @"(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})";
    }
}