using Bot.Core.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Data.ModelConfig
{
    public class PersonDataConfig : IEntityTypeConfiguration<Person>
    {
        #region Implementation of IEntityTypeConfiguration<Person>

        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            #region Id

            builder.Property(p => p.PersonId).ValueGeneratedNever().IsRequired();
            builder.HasKey(p => p.PersonId);
            builder.HasIndex(p => p.PersonId);

            #endregion

            #region User Id

            builder.Property(p => p.UserId)
                .IsRequired(false)
                .IsUnicode(false);
            builder.HasIndex(p => p.UserId);

            #endregion

            #region UserName

            // Set Max Length of username to 50 characters
            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            // Add an index to username to aid queries
            builder.HasIndex(p => p.UserName)
                .IsUnique();

            #endregion

            #region Full Name

            // Set Max Length of username to 50 characters
            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            #endregion

            #region NickName

            // Set Max Length of username to 50 characters
            builder.Property(p => p.NickName)
                .IsRequired(false)
                .HasMaxLength(100)
                .IsUnicode(false);

            #endregion

            #region Phone Number

            // Set Max Length of username to 50 characters
            builder.Property(p => p.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(20)
                .IsUnicode(false);

            #endregion

            #region Date Of Birth

            builder.Property(p => p.DateOfBirth)
                .HasColumnType("date")
                .IsRequired(false);

            #endregion

            #region Last Seen

            builder.Property(p => p.LastSeen)
                .HasColumnType("date")
                .IsRequired(false);

            #endregion

            #region Registration State

            builder.Property(p => p.RegistrationState)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            #endregion

            #region Signup Complete

            builder.Property(p => p.SignUpComplete)
                .IsRequired()
                .HasDefaultValue(false);

            #endregion

            #region Group Id

            builder.Property(g => g.GroupId)
                .IsRequired(false);

            #endregion
        }

        #endregion
    }
}