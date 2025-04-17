using Authorization.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Core.DataAccess.Configurations;

public class UserConfiguration : EntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        builder.Property(user => user.Email).IsRequired().IsUnicode().HasMaxLength(20);
        builder.Property(user => user.Password).IsRequired().HasMaxLength(100);
        builder.Property(user => user.FirstName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(20);
        builder.Property(user => user.LastName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(20);
        builder.Property(user => user.MiddleName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(20);
    }
}