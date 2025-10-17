namespace TaskManager.Application.Common.Exceptions;

public class ForbiddenAccessException: Exception
{
    public ForbiddenAccessException()
        : base("Acesso negado para a operação solicitada.") { }

    public ForbiddenAccessException(string message)
        : base(message) { }

    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException) { }
}