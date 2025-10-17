namespace TaskManager.Application.Dtos;
public record UsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public int Funcao { get; set; }
    public string NomeFuncao { get; set; } = string.Empty;
};
