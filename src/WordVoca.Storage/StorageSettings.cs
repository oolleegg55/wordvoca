namespace WordVoca.Storage;

public class StorageSettings
{
    public StorageSettings(string storageDirectory)
    {
        StorageDirectory = storageDirectory;
    }

    public string StorageDirectory { get; private set; } = string.Empty;
}
