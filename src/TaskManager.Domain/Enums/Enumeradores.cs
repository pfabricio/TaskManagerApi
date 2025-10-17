using System.ComponentModel;

namespace TaskManager.Domain.Enums;

public enum FuncaoUsuario
{
    [Description("Colaborador")]
    Colaborador = 0,
    [Description("Gerente")]
    Gerente = 1
}

public enum StatusTarefa
{
    [Description("Pendente")]
    Pendente = 0,
    [Description("Em Andamento")]
    EmAndamento = 1,
    [Description("Concluída")]
    Concluida = 2
}

public enum PrioridadeTarefa
{
    [Description("Baixa")]
    Baixa = 0,
    [Description("Média")]
    Media = 1,
    [Description("Alta")]
    Alta = 2
}