namespace Bot.Core.Di
{
    using Data.Interfaces;
    using Cored.Fabric;
    using Services.Interface;
    using UserData;

    /// <summary>
    /// Inversion of control container for the application
    /// </summary>
    public static class CoreDi
    {
        #region Service Shortcuts

        /// <summary>
        /// Shortcut to access <see cref="IBotService"/>
        /// </summary>
        public static IBotService BotServices => Fabric.Service<IBotService>();

        /// <summary>
        /// Shortcut to access <see cref="IUpdateService"/>
        /// </summary>
        public static IUpdateService UpdateServices => Fabric.Service<IUpdateService>();

        /// <summary>
        /// Shortcut to access <see cref="IMessageHandler"/>
        /// </summary>
        public static IMessageHandler MessageHandler => Fabric.Service<IMessageHandler>();

        /// <summary>
        /// Shortcut to access <see cref="IUserManager"/>
        /// </summary>
        public static IUserManager ManageUser => Fabric.Service<IUserManager>();

        /// <summary>
        /// Shortcut to access <see cref="IGroupManager"/>
        /// </summary>
        public static IGroupManager ManageGroup => Fabric.Service<IGroupManager>();

        #endregion

        #region Person and Group Data

        /// <summary>
        /// Shortcut to access <see cref="IDataService"/>
        /// </summary>
        public static IDataService DataService => Fabric.Service<IDataService>();

        /// <summary>
        /// Shortcut to access IReadService
        /// </summary>
        public static IReadService<Person> ReadPersonService => Fabric.Service<IReadService<Person>>();

        /// <summary>
        /// Shortcut to access IReadService
        /// </summary>
        public static IReadService<Group> ReadGroupService => Fabric.Service<IReadService<Group>>();

        /// <summary>
        /// Shortcut to access <see cref="IWriteService"/>
        /// </summary>
        public static IWriteService WriteService => Fabric.Service<IWriteService>();

        #endregion

        #region Database Shortcuts

        /// <summary>
        /// Shortcut to access the <see cref="IBotDataService"/> methods
        /// </summary>
        public static IBotDataService BotDataService => Fabric.Service<IBotDataService>();

        /// <summary>
        /// Shortcut to access the <see cref="IReadDataService"/> methods
        /// </summary>
        public static IReadDataService ReadDataService => Fabric.Service<IReadDataService>();

        /// <summary>
        /// Shortcut to access the <see cref="IWriteDataService"/> methods
        /// </summary>
        public static IWriteDataService WriteDataService => Fabric.Service<IWriteDataService>();

        #endregion
    }
}