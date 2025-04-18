using Authorization.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Core.DataAccess.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        const string sqlQuery = "now()";
        builder.HasKey(user => user.Id);
        builder.Property(user => user.CreatedAt).HasDefaultValueSql(sqlQuery);
        builder.Property(user => user.UpdatedAt).HasDefaultValueSql(sqlQuery);
    }
}