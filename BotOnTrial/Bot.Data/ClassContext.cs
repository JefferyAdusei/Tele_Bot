using Bot.Core.UserData;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data
{
    public class ClassContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassContext"/> class.
        /// </summary>
        /// <param name="options">The dbContext configurations</param>
        public ClassContext(DbContextOptions<ClassContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Gets or sets the Groups table for CRUD operations
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the persons table for CRUD operations
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        #endregion

        #region Overrides of DbContext

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Relationship configurations

            //// One-to-many relationship between person and a group.
            //modelBuilder.Entity<Person>()
            //    .HasOne(p => p.Group)
            //    .WithMany(g => g.Person)
            //    .IsRequired(false);


            #endregion
        }

        #endregion
    }
}