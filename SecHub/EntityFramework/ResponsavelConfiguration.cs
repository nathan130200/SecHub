using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecHub.EntityFramework.Abstractions;
using SecHub.Enums;
using SecHub.Models;

namespace SecHub.EntityFramework;

public class ResponsavelConfiguration : BaseEntityTypeConfiguration<Responsavel>
{
    protected override void Configure(EntityTypeBuilder<Responsavel> builder)
    {
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(EFConstants.MaxNomeLength);
        builder.Property(x => x.Genero).HasDefaultValue(PessoaGenero.Outro);
        builder.Property(x => x.NumCpf).IsRequired(false).HasMaxLength(EFConstants.MaxCpfLength);
        builder.Property(x => x.GrauParentesco).IsRequired(true).HasMaxLength(EFConstants.MaxDefaultStringLength);

        builder.Property(x => x.Telefones)
            .IsRequired(false)
            .HasConversion(
                s => string.Join(';', s),
                s => new(s.Split(';', StringSplitOptions.RemoveEmptyEntries)),
                new ValueComparer<HashSet<string>>(
                    (x, y) => x.SequenceEqual(y),
                    x => x.Aggregate(0, (x, y) => HashCode.Combine(x, y)),
                    x => new(x.ToArray())
                )
            );
    }
}
