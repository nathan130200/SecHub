using SecHub.Enums;

namespace SecHub.Rest.Responsaveis;

public class UpdateResponsavelDto
{
    public bool? Ativo { get; set; }
    public string? Nome { get; set; }
    public string? NumCpf { get; set; }
    public PessoaGenero? Genero { get; set; }
    public string? GrauParentesco { get; set; }
    public HashSet<string>? Telefones { get; set; }
}
