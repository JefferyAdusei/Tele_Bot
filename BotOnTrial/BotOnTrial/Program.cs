namespace BotOnTrial
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Bot.Core;
    using Bot.Core.ChatStart;
    using Bot.Core.Di;
    using Bot.Core.Schedules;
    using Bot.Data;
    using Cored.Fabric;
    using Cored.Fabric.Construction;
    using Cored.Fabric.Di;
    using Cored.Logging;
    using Microsoft.Extensions.Logging;

    class Program
    {
        static async Task Main()
        {
            // Setup application
            await ApplicationSetupAsync();

            //When application is configured, start the bot
            try
            {
                // Start the bot
                Startup.Start();

                FabricDi.Logger.LogInformation("Application started successfully");
                FabricDi.Logger.LogCriticalSource("Critical message");

                // Start the scheduler
                Schedule.Begin();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region Application Configuration

        /// <summary>
        /// Configure all necessary bindings
        /// </summary>
        /// <returns></returns>
        private static async Task ApplicationSetupAsync()
        {
            // Ensure the Data Directory exists
            if (!Directory.Exists("Database"))
            {
                Directory.CreateDirectory("Database");
            }

            // Setup fabric
            Fabric.Construct<LocalFabricConstruction>()
                .AddXmlLogger("Logs\\log.xml")
                .AddDataService()
                .AddBotServices()
                //.AddBotDataService()
                .Build();

            // TODO: Remove this method when ready.
            //await CoreDi.DataService.ClearAllDataAsync();

            await CoreDi.DataService.EnsureDataCreatedAsync();
            // Ensure that the database is setup and running correctly.
            //await CoreDi.BotDataService.EnsureDataCreatedAsync();
        }

        #endregion
    }
}
