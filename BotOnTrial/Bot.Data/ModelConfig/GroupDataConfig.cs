using Bot.Core.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Data.ModelConfig
{
    /// <summary>
    /// Configurations for the group entity
    /// </summary>
    public class GroupDataConfig : IEntityTypeConfiguration<Group>
    {
        #region Implementation of IEntityTypeConfiguration<Group>

        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            #region Group Id

            builder.HasKey(g => g.GroupId);

            builder.Property(g => g.GroupId)
                .ValueGeneratedNever().IsRequired();
            builder.HasIndex(g => g.GroupId);

            #endregion

            #region Group Name

            builder.Property(g => g.GroupName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            #endregion

            #region Group Description

            builder.Property(g => g.Description)
                .IsRequired(false)
                .HasMaxLength(1024)
                .IsUnicode(false);

            #endregion

            #region Group Invite Link

            // Set Max Length of username to 50 characters
            builder.Property(g => g.InviteLink)
                .IsRequired(false)
                .HasMaxLength(256)
                .IsUnicode(false);

            #endregion
        }

        #endregion
    }
}