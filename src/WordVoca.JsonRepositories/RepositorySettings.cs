namespace WordVoca.Storage;

public class RepositorySettings
{
    public RepositorySettings(string storageDirectory)
    {
        StorageDirectory = storageDirectory;
    }

    public string StorageDirectory { get; private set; } = string.Empty;
}
