using Bot.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Data.ModelConfig
{
    /// <summary>
    /// Fluent API configuration for <see cref="BotDataModel"/>
    /// </summary>
    public class BotDataConfig : IEntityTypeConfiguration<BotDataModel>
    {
        #region Implementation of IEntityTypeConfiguration<BotDataModel>

        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<BotDataModel> builder)
        {
            // Set Id as primary key
            builder.HasKey(a => a.Id);

            #region UserName

            // Set Max Length of username to 50 characters
            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            // Add an index to username to aid queries
            builder.HasIndex(a => a.Username)
                .IsUnique();

            #endregion

            #region Firstname

            // Set Max Length of username to 50 characters
            builder.Property(a => a.Firstname)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            #endregion

            #region TeamAccount

            builder.Property(a => a.TeamAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);

            #endregion

            #region Skype

            builder.Property(a => a.Skype)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false);

            #endregion

            #region Mail Account

            builder.Property(a => a.MailAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(false);

            #endregion

            #region Phone

            builder.Property(a => a.Phone)
                .HasMaxLength(15)
                .IsRequired(false)
                .IsUnicode(false);

            #endregion

            #region Role

            builder.Property(a => a.Role)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false);

            #endregion

            #region Is Deleted

            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            #endregion

            #region PassCode

            builder.Property(a => a.PassCode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            #endregion
        }

        #endregion
    }
}