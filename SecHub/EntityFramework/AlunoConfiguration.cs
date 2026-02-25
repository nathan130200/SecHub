using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecHub.EntityFramework.Abstractions;
using SecHub.Models;

namespace SecHub.EntityFramework;

public class AlunoConfiguration : BaseEntityTypeConfiguration<Aluno>
{
    protected override void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(EFConstants.MaxNomeLength);

        builder.Property(x => x.NumCpf).IsRequired(false).HasMaxLength(EFConstants.MaxCpfLength);

        builder.Property(x => x.NumBolsaFamilia).IsRequired(false).HasMaxLength(EFConstants.MaxDefaultStringLength);

        builder.Property(x => x.NumEducacenso).IsRequired(false).HasMaxLength(EFConstants.MaxDefaultStringLength);

        builder.Property(x => x.IsBrasileiro).HasDefaultValue(true);

        builder.Property(x => x.NaturalidadeEstado).IsRequired(false).HasMaxLength(EFConstants.MaxDefaultStringLength);

        builder.Property(x => x.NaturalidadeMunicipio).IsRequired(false).HasMaxLength(EFConstants.MaxDefaultStringLength);

        builder.Ignore(x => x.Idade);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NumCpf).IsUnique(true);
        builder.HasIndex(x => x.NumEducacenso).IsUnique(true);
        builder.HasIndex(x => x.NumBolsaFamilia).IsUnique(true);

        builder.HasMany(x => x.Responsaveis)
            .WithMany(x => x.Alunos)
            .UsingEntity(
                lhs => lhs.HasOne(typeof(Responsavel)).WithMany().HasForeignKey("ResponsavelId"),
                rhs => rhs.HasOne(typeof(Aluno)).WithMany().HasForeignKey("AlunoId"),
                table => table.HasKey("AlunoId", "ResponsavelId")
            );
    }
}