namespace SecHub.Models.Abstractions;

public class BaseEntity
{
    public int Id { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriado { get; set; }

    public DateTime DataAtualizado { get; set; }
}
