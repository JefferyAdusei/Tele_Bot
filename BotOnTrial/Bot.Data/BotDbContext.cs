using Bot.Core.Data;
using Bot.Data.ModelConfig;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data
{
    public class BotDbContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BotDbContext"/>
        /// </summary>
        /// <param name="options"></param>
        public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Gets or sets the Database for queries.
        /// </summary>
        public DbSet<BotDataModel> BotData { get; set; }

        #endregion

        #region Overrides of DbContext

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Fluent API configuration for the data.
            modelBuilder.ApplyConfiguration(new BotDataConfig());

            // Configure soft delete
            modelBuilder.Entity<BotDataModel>()
                .HasQueryFilter(a => !a.IsDeleted);
        }

        #endregion
    }
}