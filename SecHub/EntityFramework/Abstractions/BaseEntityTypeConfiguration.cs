using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecHub.Models;

namespace SecHub.EntityFramework.Abstractions;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    void IEntityTypeConfiguration<TEntity>.Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Ativo).HasDefaultValue(true);

        builder.Property(x => x.DataCriado).ValueGeneratedOnAdd();

        builder.Property(x => x.DataAtualizado).ValueGeneratedOnAddOrUpdate();

        Configure(builder);
    }

    protected abstract void Configure(EntityTypeBuilder<TEntity> builder);
}