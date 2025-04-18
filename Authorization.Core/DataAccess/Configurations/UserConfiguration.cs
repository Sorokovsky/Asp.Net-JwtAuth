using Authorization.Core.DataAccess.Entities;
using Authorization.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Core.DataAccess.Configurations;

public class UserConfiguration : EntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        base.Configure(builder);
        builder.Property(user => user.Email).IsRequired().IsUnicode().HasMaxLength(UserModel.EmailMaxLength);
        builder.Property(user => user.Password).IsRequired().HasMaxLength(UserModel.PasswordMaxLength);
        builder.Property(user => user.FirstName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(UserModel.FirstNameMaxLength);
        builder.Property(user => user.LastName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(UserModel.LastNameMaxLength);
        builder.Property(user => user.MiddleName).IsRequired(false).HasDefaultValue(string.Empty).HasMaxLength(UserModel.MiddleNameMaxLength);
    }
}