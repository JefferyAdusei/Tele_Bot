using System;
using Bot.Core.ChatBot;
using Bot.Core.Di;
using Bot.Core.UserData;

namespace Bot.Core.Schedules
{
    using System.Threading.Tasks;
    using Quartz;
 
    public class BirthdayJob : IJob
    {
        #region Implementation of IJob

        /// <inheritdoc />
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Starting birthday job...");
                // Get the list of all persons
                var persons = await CoreDi.ReadPersonService.RetrieveAllAsync();

                // If we don't have any person in the database, return
                if (persons is null)
                {
                    return;
                }

                // Get the current date
                var today = DateTime.Today;

                await foreach (Person person in persons)
                {
                    if (person.DateOfBirth.Month == today.Month && person.DateOfBirth.Day == today.Day)
                    {
                        // Send a personal message to the person
                        await CoreDi.BotServices.Client.SendReplyAsync(person.PersonId,
                                                                   $"Happy Birthday {person.NickName}, I hope I am the first...");
                        // Send message to group.
                        await CoreDi.BotServices.Client.SendReplyAsync(person.GroupId,
                                                                   $"Help me celebrate {person.NickName}. Have a blast today!!!");
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