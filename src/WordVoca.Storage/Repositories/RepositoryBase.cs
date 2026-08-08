namespace WordVoca.Storage.Repositories;

public class RepositoryBase
{
    protected readonly TimeProvider TimeProvider;

    public RepositoryBase(TimeProvider timeProvider)
    {
        TimeProvider = timeProvider;
    }

    protected DateTimeOffset ToDateTimeOffset(DateTime utcDateTime)
    {
        return new DateTimeOffset(DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc), TimeSpan.Zero)
            .ToOffset(TimeProvider.GetLocalNow().Offset);
    }
}
