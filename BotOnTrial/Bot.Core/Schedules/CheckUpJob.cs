using System;
using Bot.Core.ChatBot;
using Bot.Core.Di;

namespace Bot.Core.Schedules
{
    using System.Threading.Tasks;
    using Quartz;

    public class CheckUpJob : IJob
    {
        #region Implementation of IJob

        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Get all members
                var persons = await CoreDi.ReadPersonService.RetrieveAllAsync();

                // Return if there are no persons
                if (persons is null)
                {
                    return;
                }

                // Go through the list of members
                await foreach (var person in persons)
                {
                    // If a member has not been active for a weeks, send the member a message.
                    TimeSpan difference = person.LastSeen - DateTime.Now;
                    if (difference.Days % 7 == 0)
                    {
                        await CoreDi.BotServices.Client.SendHtmlReplyAsync(person.PersonId,
                                                                    $"<b>Hi {person.NickName}</b>, Its been long I heard from you in the group\n" +
                                                                    "Please do well to say something on the page so that I know you are doing well");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        #endregion
    }
}