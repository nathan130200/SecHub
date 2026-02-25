using SecHub.Rest.Responsaveis;
using System.Text.Json.Serialization;

namespace SecHub.Rest.Alunos;

public class GetAlunoDto : CreateAlunoDto
{
    public int Idade { get; set; }

    public DateTime DataCriado { get; set; }
    public DateTime DataAtualizado { get; set; }

    [JsonPropertyOrder(1)]
    public List<GetResponsavelDto>? Responsaveis { get; set; }
}
