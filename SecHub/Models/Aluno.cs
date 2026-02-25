using SecHub.Enums;
using SecHub.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecHub.Models;

public class Aluno : BaseEntity
{
    public string Nome { get; set; }

    public DateOnly DataNascimento { get; set; }

    public PessoaGenero Genero { get; set; }

    public string? NumCpf { get; set; }

    public string? NumEducacenso { get; set; }

    public string? NumBolsaFamilia { get; set; }

    public bool IsBrasileiro { get; set; }

    public string NaturalidadeEstado { get; set; }

    public string NaturalidadeMunicipio { get; set; }

    public virtual ICollection<Responsavel>? Responsaveis { get; set; }

    [NotMapped]
    public int Idade
    {
        get
        {
            var dataLimite = new DateTime(DateTime.Now.Year, 3, 31);

            var dataNasc = DataNascimento.ToDateTime(new(12, 0, 0));

            int idadeNaData = dataLimite.Year - dataNasc.Year;

            if (dataNasc > dataLimite.AddYears(-idadeNaData))
                idadeNaData--;

            return idadeNaData;
        }
    }
}