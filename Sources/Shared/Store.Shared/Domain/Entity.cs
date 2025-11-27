namespace Store.Shared;

public abstract class Entity
{
    public virtual string Id { get; init; } = EntityId.New();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted() => DeletedAt is not null;
    public virtual void MarkAsDeleted() => DeletedAt = DateTime.UtcNow;
}