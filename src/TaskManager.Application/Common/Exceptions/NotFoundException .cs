namespace TaskManager.Application.Common.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string name, object? key = null)
        : base(GenerateMessage(name, key))
    { }

    private static string GenerateMessage(string name, object? key)
    {
        return key == null
            ? $"Recurso '{name}' não foi encontrado."
            : $"Recurso '{name}' com identificador '{key}' não foi encontrado.";
    }
}