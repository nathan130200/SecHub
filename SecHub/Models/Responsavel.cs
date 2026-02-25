using SecHub.Enums;
using SecHub.Models.Abstractions;

namespace SecHub.Models;

public class Responsavel : BaseEntity
{
    public string Nome { get; set; }

    public string? NumCpf { get; set; }

    public PessoaGenero Genero { get; set; }

    public string GrauParentesco { get; set; }

    public HashSet<string> Telefones { get; set; }

    public virtual ICollection<Aluno> Alunos { get; set; }
}