namespace TaskManager.Application.Dtos;

public record ComentarioDto
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public int UsuarioId { get; set; }
    public string NomeTarefa { get; set; } = string.Empty;
    public string NomeUsuario { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}