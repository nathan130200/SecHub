using SecHub.Enums;

namespace SecHub.Rest.Alunos;

public class CreateAlunoDto
{
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public PessoaGenero Genero { get; set; }
    public string? NumCpf { get; set; }
    public string? NumEducacenso { get; set; }
    public string? NumBolsaFamilia { get; set; }
    public bool IsBrasileiro { get; set; }
    public string? NaturalidadeEstado { get; set; }
    public string? NaturalidadeMunicipio { get; set; }
}