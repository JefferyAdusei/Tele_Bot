using Bot.Core.Services;
using Bot.Core.Services.Interface;
using Cored.Fabric.Construction;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Core
{
    public static class ServiceConstruction
    {
        public static FabricConstruction AddBotServices(this FabricConstruction construction)
        {
            // Bind user manager
            construction.ServiceCollection.AddTransient<IUserManager, UserManager>();

            // Bind group manager
            construction.ServiceCollection.AddTransient<IGroupManager, GroupManager>();

            // Bind message handler
            construction.ServiceCollection.AddTransient<IMessageHandler, MessageHandler>();

            // Bind update service
            construction.ServiceCollection.AddTransient<IUpdateService, UpdateService>();

            // Bind bot service
            construction.ServiceCollection.AddSingleton<IBotService, BotService>();

            return construction;
        }
    }
}