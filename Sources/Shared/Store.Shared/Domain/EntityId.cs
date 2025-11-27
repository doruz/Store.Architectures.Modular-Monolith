namespace Store.Shared;

public sealed record EntityId(string Id)
{
    public static EntityId New() => Guid.NewGuid().ToString().ToLower();

    public static implicit operator EntityId(string id) => new(id);
    public static implicit operator string(EntityId id) => new(id.Id);
}