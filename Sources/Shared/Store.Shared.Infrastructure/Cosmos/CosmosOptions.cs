namespace Store.Shared.Infrastructure.Cosmos;

public abstract record CosmosOptions
{
    public required string DatabaseName { get; init; }

    public required int MaxThroughput { get; init; }
}