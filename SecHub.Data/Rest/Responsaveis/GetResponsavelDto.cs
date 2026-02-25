namespace SecHub.Rest.Responsaveis;

public class GetResponsavelDto : CreateResponsavelDto
{
    public DateTime DataCriado { get; set; }
    public DateTime DataAtualizado { get; set; }
}