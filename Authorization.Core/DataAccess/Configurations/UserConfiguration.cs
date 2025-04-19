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
        builder.ComplexProperty(user => user.FullName, propertyBuilder =>
        {
            propertyBuilder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(FullName.FirstNameMaxLength)
                .HasDefaultValue(string.Empty);
            propertyBuilder.Property(x => x.MiddleName).HasColumnName("middle_name")
                .HasMaxLength(FullName.MiddleNameMaxLength)
                .HasDefaultValue(string.Empty);
            propertyBuilder.Property(x => x.LastName).HasColumnName("last_name")
                .HasMaxLength(FullName.LastNameMaxLength)
                .HasDefaultValue(string.Empty);
        });
    }
}