using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Store.Shared.Infrastructure.Cosmos;

public abstract class CosmosDatabase(CosmosClient cosmosClient, IOptions<CosmosOptions> options)
    : IAppInitializer
{
    protected abstract IReadOnlyList<ContainerProperties> Containers { get; }

    public async Task Execute()
    {
        try
        {
            await InitializeContainers(await InitializeDatabase());
        }
        catch
        {
            // log details when database fails to be initialized
        }
    }

    protected Container GetContainer(string containerName)
        => cosmosClient.GetContainer(options.Value.DatabaseName, containerName);

    private async Task<Database> InitializeDatabase()
    {
        var throughput = ThroughputProperties.CreateAutoscaleThroughput(options.Value.MaxThroughput);

        return await cosmosClient.CreateDatabaseIfNotExistsAsync(options.Value.DatabaseName, throughput);
    }

    private async Task InitializeContainers(Database database)
    {
        foreach (var container in Containers)
        {
            await database.CreateContainerIfNotExistsAsync(container);
        }
    }
}