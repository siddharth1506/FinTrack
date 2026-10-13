namespace FinTrack.Domain.Common;

public abstract class BaseEntity
{
    public Guid id {get;protected set;} = Guid.NewGuid();
    public DateTime Createdatutc  {get; protected set;} = DateTime.UtcNow;

    public DateTime? updatedAtUtc {get; protected set;} = DateTime.UtcNow;  

    protected void  MarkUpdated() => updatedAtUtc = DateTime.UtcNow;
}