using Bot.Core.Data.Interfaces;
using Bot.Core.UserData;
using Bot.Data.DataService;
using Cored.Fabric.Construction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Data
{
    /// <summary>
    /// Extension methods for the <see cref="FabricConstruction"/>
    /// </summary>
    public static class DataConstruction
    {
        #region Bot Data Construction

        /// <summary>
        /// Bind implementations of all data service into the fabric construction
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FabricConstruction AddBotDataService(this FabricConstruction construction)
        {
            // Inject our SQLite EF data store
            construction.ServiceCollection
                .AddDbContext<BotDbContext>
                    (
                     options => options.UseSqlite(construction.Configuration.GetConnectionString("BotConnection"))
                    );

            // Add client data store for easy access/use of the backing data store
            construction.ServiceCollection
                .AddScoped<IBotDataService>
                    (
                     provider => new BotDataService(provider.GetService<BotDbContext>())
                    );

            // Bind Read Data Service
            construction.ServiceCollection
                .AddScoped<IReadDataService>
                    (
                     provider => new ReadDataService(provider.GetService<BotDbContext>())
                    );

            // Bind Write Data Service
            construction.ServiceCollection
                .AddScoped<IWriteDataService>
                    (
                     provider => new WriteDataService(provider.GetService<BotDbContext>())
                    );

            // Return construction for chaining.
            return construction;
        }

        #endregion

        #region Class Construction

        /// <summary>
        /// Bind implementations of all data service into the fabric construction
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FabricConstruction AddDataService(this FabricConstruction construction)
        {
            // Inject our SQLite EF data store
            construction.ServiceCollection
                .AddDbContext<ClassContext>
                    (
                     options => options.UseSqlite(construction.Configuration.GetConnectionString("ClassConnection"))
                    );

            // Add client data store for easy access/use of the backing data store
            construction.ServiceCollection
                .AddScoped<IDataService>
                    (
                     provider => new DataServices(provider.GetService<ClassContext>())
                    );

            // Bind Read Person data Service
            construction.ServiceCollection
                .AddScoped<IReadService<Person>>
                    (
                     provider => new PersonReadService(provider.GetService<ClassContext>())
                    );

            // Bind Read Group data Service
            construction.ServiceCollection
                .AddScoped<IReadService<Group>>
                    (
                     provider => new GroupReadService(provider.GetService<ClassContext>())
                    );

            // Bind Write Data Service
            construction.ServiceCollection
                .AddScoped<IWriteService>
                    (
                     provider => new WriteService(provider.GetService<ClassContext>())
                    );

            // Return construction for chaining.
            return construction;
        }

        #endregion
    }
}