namespace WordVoca.Core.Exceptions;

public class WordListExistsException : Exception
{
    public WordListExistsException()
    {
    }

    public WordListExistsException(string? wordList) : base($"Word list with \"{wordList}\" exists")
    {
    }
}
