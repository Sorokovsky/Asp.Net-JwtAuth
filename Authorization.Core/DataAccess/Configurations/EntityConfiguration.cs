﻿using Authorization.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Core.DataAccess.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.CreatedAt).HasDefaultValue(DateTime.UtcNow);
        builder.Property(user => user.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
    }
}