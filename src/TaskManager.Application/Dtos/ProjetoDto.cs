namespace TaskManager.Application.Dtos;

public record ProjetoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CriadoPor { get; set; }
}