namespace TaskManager.Application.Dtos;

public record RelatorioDesempenhoDto(
    int UsuarioId,
    string Nome,
    int TarefasConcluidas,
    double MediaTarefasPorDia
);