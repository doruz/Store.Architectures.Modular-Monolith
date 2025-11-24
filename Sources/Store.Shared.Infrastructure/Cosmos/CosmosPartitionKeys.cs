using Microsoft.Azure.Cosmos;

namespace Store.Shared.Infrastructure.Cosmos;

public static class CosmosPartitionKeys
{
    public static PartitionKey ToPartitionKey(this string key) => new(key);
}