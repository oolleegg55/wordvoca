namespace WordVoca.Core.Exceptions;

public class UnsupportedLanguageException : Exception
{
    public UnsupportedLanguageException()
    {
    }

    public UnsupportedLanguageException(string? message) : base(message)
    {
    }
}
